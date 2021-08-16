using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Playground.Core.Events;
using Playground.Router.Clients;
using Playground.Router.Old;
using Playground.Xml.Serialization;
using Playground.XsltTransform;

namespace Playground.Router {
    public class RouterEngine : Engine {
        private readonly TerminalStore _store;
        private readonly ClientFactory _factory;
        private readonly TerminalExtensions _extensions;
        private readonly EventHub _eventHub;

        public RouterEngine(EventHub eventHub, TerminalStore store, TerminalExtensions extensions, ClientFactory factory) {
            _store = store;
            _eventHub = eventHub;
            _extensions = extensions;
            _factory = factory;
        }

        public async Task<List<string>> ProcessAsync<T>(Guid transactionId, Old.Request<T> request, CancellationToken cancellationToken)
            where T : class =>
            await TerminalProcessor<T>.ProcessAsync(_eventHub, transactionId, request, _store, _extensions, _factory, cancellationToken);

        public async Task<Response> ProcessAsync2(Request request, CancellationToken cancellationToken) =>
            await Transaction.ProcessAsync(request,  _factory, cancellationToken);
    }

    public class Transaction {
        private Request _request;
        private Response _response;
        private ClientFactory _clientFactory;
        private CancellationToken _cancellationToken;
        
        private string _requestXml;
        private readonly Dictionary<string, object> _extensions;
        
        private Transaction(Request request, ClientFactory clientFactory, CancellationToken cancellationToken) {
            _request = request;
            _clientFactory = clientFactory;
            _cancellationToken = cancellationToken;
            
            _response = new Response();
            _extensions = request.Extensions?.GetExtensions() ?? new Dictionary<string, object>();
        }
        
        public static async Task<Response> ProcessAsync(Request request, ClientFactory clientFactory, CancellationToken cancellationToken) =>
            await new Transaction(request, clientFactory, cancellationToken).ProcessAsync();

        private async Task<Response> ProcessAsync() {
            _requestXml = SerializeRequest();
            
            foreach (var terminal in _request.Terminals) {
                if (await TryProcessAsync(terminal))
                    break;

                await Task.Delay(100, _cancellationToken);
            }
            
            return _response;
        }
        
        private string SerializeRequest() {
            var xml = XDocument.Parse(_request.Serialize());

            if (!PayloadIsXml()) return xml.ToString();

            var n = xml.XPathSelectElement("request/payload");
            n!.RemoveAll();
            n.Add(XDocument.Parse($"{_request.Payload}").Root!);

            return xml.ToString();

            bool PayloadIsXml() =>
                _request.Payload.StartsWith("<");
        }

        private async Task<bool> TryProcessAsync(Terminal terminal) {
            try { return await ProcessAsync(terminal); }
            catch { return false; }
        }

        private async Task<bool> ProcessAsync(Terminal terminal) {
            var request = Transformer.Transform(_requestXml, terminal.Xslt, _extensions);
            var client = _clientFactory.Create(terminal);
            var responseXml = await client.SendAsync(GetConfiguration(terminal), request, _cancellationToken);
            
            return true;
        }
        
        private Configuration GetConfiguration(Terminal terminal) {
            var configXml = Transformer.Transform($"<request name='{_request.Name}'><config/></request>", terminal.Xslt!, _extensions);
            var config = new Configuration();

            if (!string.IsNullOrWhiteSpace(configXml))
                config = Serializer.DeSerialize<Configuration>(configXml) ?? new Configuration();

            config.Settings.AddRange(terminal.Settings);
            
            var xsltSettings = config.Settings.Where(s => !terminal.Settings.Any(t => t.Name.Equals(s.Name)));

            var newConfig = new Configuration { Settings = terminal.Settings.ToList() };
            newConfig.Settings.AddRange(xsltSettings);
            newConfig.Settings.Add(new Setting("TransactionId", $"{_request.TransactionId}"));
            newConfig.Settings.Add(new Setting("Terminal", $"{_request.Name}"));

            return newConfig;
        }
    }

    public class Response {
            
    }
        
        [XmlRoot("request")]
        public class Request {
            [XmlIgnore] public Guid TransactionId { get; set; } = Guid.NewGuid();
            [XmlAttribute("name")] public string Name { get; set; } = "request";
            [XmlElement("payload")] public string Payload { get; set; } = string.Empty;
            [XmlIgnore] public IEnumerable<Terminal> Terminals { get; set; } = new Terminal[0];
            [XmlIgnore] public TerminalExtensions? Extensions { get; set; }
        }
    }