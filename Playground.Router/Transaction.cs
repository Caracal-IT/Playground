using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Playground.Router.Clients;
using Playground.Xml;
using Playground.Xml.Serialization;
using Playground.XsltTransform;
using Playground.XsltTransform.Extensions;

using static Playground.Xml.Serialization.Serializer;

namespace Playground.Router {
    internal class TransactionFactory<T> where T:class{
        private readonly Request<T> _request;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;
        private readonly string _requestXml;
        
        public TransactionFactory(Request<T> request, ClientFactory factory, TerminalExtensions extensions) {
            _request = request;
            _factory = factory;
            _extensions = extensions.GetExtensions();
            _requestXml = SerializeRequest();
        }

        public Transaction<T> Create(Terminal terminal) =>
            new (_request.Name, _requestXml, terminal, _factory, _extensions);
        
        private string SerializeRequest() {
            var reqStr = Serialize(_request);
            var reqXml = XDocument.Parse(reqStr);

            if (!_request.Payload!.ToString()!.StartsWith("<")) return reqXml.ToString();

            var n = reqXml.XPathSelectElement("request/payload");
            n!.RemoveAll();
            n.Add(XDocument.Parse(_request.Payload.ToString()!).Root!);

            return reqXml.ToString();
        }
    }
    
    internal class Transaction<T> where T : class { 
        private readonly string _requestName;
        private readonly string _requestXml;
        private readonly Terminal _terminal;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;

        public Transaction(
            string requestName,
            string requestXml,
            Terminal terminal,
            ClientFactory factory,
            Dictionary<string, object> extensions) 
        {
            _requestName = requestName;
            _requestXml = requestXml;
            _terminal = terminal;
            _factory = factory;
            _extensions = extensions;
        }
        
        public async Task<string?> ProcessAsync(CancellationToken cancellationToken) {
            var client = _factory.Create(_terminal);
            var responseXml = await client.SendAsync(GetConfiguration(), GetClientMessage(), _terminal, cancellationToken);

            return ProcessResponseMessage(responseXml);
        }

        private string GetClientMessage() => Transformer.Transform(_requestXml, _terminal.Xslt!, _extensions);

        private Configuration GetConfiguration() {
            var configXml = Transformer.Transform($"<request name='{_requestName}'><config/></request>", _terminal.Xslt!, _extensions);
            var config = new Configuration();

            if (!string.IsNullOrWhiteSpace(configXml))
                config = Serializer.DeSerialize<Configuration>(configXml) ?? new Configuration();

            return config;
        }

        private string? ProcessResponseMessage(string response) {
            var xml = WrapXmlInResponseTag(response).Transform(_terminal.Xslt!, _extensions)
                .ToXml();

            if (HasFailed(xml!))
                return null;

            return xml!.FirstNode?.ToString() ?? string.Empty;
        }

        private string WrapXmlInResponseTag(string xmlStr) =>
            $"<request name='{_requestName}'>{xmlStr.ToXml()}</request>";

        private static bool HasFailed(XContainer xml) {
            if (xml?.Nodes().Count() > 1)
                return !Serializer.DeSerialize<Response>(xml.LastNode!.ToString())?.Success ?? true;

            return false;
        }
    }
}