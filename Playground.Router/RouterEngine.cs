using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using Playground.Router.Clients;
using Playground.Xml.Serialization;
using Playground.XsltTransform;
using Playground.XsltTransform.Extensions;

namespace Playground.Router {
    public class RouterEngine : Engine {
        private readonly ClientFactory _factory;

        public RouterEngine(ClientFactory factory) => _factory = factory;

        public async Task<IEnumerable<Response>> ProcessAsync(Request request, CancellationToken cancellationToken) =>
            await Transaction.ProcessAsync(request,  _factory, cancellationToken);
    }

    public class Transaction {
        private Request _request;
        private List<Response> _responses;
        private ClientFactory _clientFactory;
        private CancellationToken _cancellationToken;
        
        private string _requestXml;

        private Transaction(Request request, ClientFactory clientFactory, CancellationToken cancellationToken) {
            _request = request;
            _clientFactory = clientFactory;
            _cancellationToken = cancellationToken;
            
            _responses = new List<Response>();
            _requestXml = string.Empty;
        }
        
        public static async Task<IEnumerable<Response>> ProcessAsync(Request request, ClientFactory clientFactory, CancellationToken cancellationToken) =>
            await new Transaction(request, clientFactory, cancellationToken).ProcessAsync();

        private async Task<IEnumerable<Response>> ProcessAsync() {
            _requestXml = SerializeRequest();
            
            foreach (var terminal in _request.Terminals) {
                if (await TryProcessAsync(terminal))
                    break;

                await Task.Delay(100, _cancellationToken);
            }
            
            return _responses;
        }
        
        private string SerializeRequest() {
            var xml = XDocument.Parse(_request.ToXml());

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
            var request = _requestXml.Transform(terminal.Xslt, _request.Extensions);
            var client = _clientFactory.Create(terminal);
            var responseXml = WrapResponse(await client.SendAsync(GetConfiguration(terminal), request, _cancellationToken));
            var result = responseXml.Transform(terminal.Xslt, _request.Extensions).ToXDocument();
            var response = result.Root!.FirstNode!.ToObject<TerminalResponse>();
            
            _responses.Add(new Response {
                Result = result.Root!.LastNode!.ToString(),
                TerminalResponse = response
            });
            
            return response.Success;

            string WrapResponse(string xml) => $"<request name='{_request.Name}'>{xml.ToXDocument().Root}</request>";
        }


        private Configuration GetConfiguration(Terminal terminal) {
            var configXml = Transformer.Transform($"<request name='{_request.Name}'><config/></request>", terminal.Xslt, _request.Extensions);
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

    [XmlRoot("terminal-response")]
    public class TerminalResponse {
        [XmlAttribute("success")] public bool Success { get; set; } = true;
        [XmlAttribute("code")] public string Code { get; set; } = "00";
        [XmlAttribute("reference")] public string Reference { get; set; } = string.Empty;
    }
    
    [XmlRoot("response")]
    public class Response {
        public TerminalResponse TerminalResponse { get; set; } = new();
        public string Result { get; set; } = string.Empty;
    }
        
        [XmlRoot("request")]
        public class Request {
            [XmlIgnore] public Guid TransactionId { get; set; } = Guid.NewGuid();
            [XmlAttribute("name")] public string Name { get; set; } = "request";
            [XmlElement("payload")] public string Payload { get; set; } = string.Empty;
            [XmlIgnore] public IEnumerable<Terminal> Terminals { get; set; } = Array.Empty<Terminal>();
            [XmlIgnore] public Dictionary<string, object> Extensions { get; set; } = new();
        }
    }