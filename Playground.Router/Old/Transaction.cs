using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Playground.Core.Events;
using Playground.Router.Clients;
using Playground.Xml;
using Playground.Xml.Serialization;
using Playground.XsltTransform;
using Playground.XsltTransform.Extensions;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.Router.Old {
    internal class TransactionFactory<T> where T:class{
        private readonly Request<T> _request;
        private readonly EventHub _eventHub;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;
        private readonly string _requestXml;
        
        public TransactionFactory(EventHub eventHub, Request<T> request, ClientFactory factory, TerminalExtensions extensions) {
            _request = request;
            _factory = factory;
            _eventHub = eventHub;
            _extensions = extensions.GetExtensions();
            _requestXml = SerializeRequest();
        }

        public Transaction Create(Guid transactionId, OldTerminal terminal) =>
            new (_eventHub, transactionId, _request.Name, _requestXml, terminal, _factory, _extensions);
        
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
    
    internal class Transaction {
        private EventHub _eventHub;
        private readonly Guid _transactionId;
        private readonly string _requestName;
        private readonly string _requestXml;
        private readonly OldTerminal _terminal;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;

        public Transaction(
            EventHub eventHub,
            Guid transactionId,
            string requestName,
            string requestXml,
            OldTerminal terminal,
            ClientFactory factory,
            Dictionary<string, object> extensions) 
        {
            _eventHub = eventHub;
            _transactionId = transactionId;
            _requestName = requestName;
            _requestXml = requestXml;
            _terminal = terminal;
            _factory = factory;
            _extensions = extensions;
        }
        
        public Task<(string? message ,bool success)> ProcessAsync(CancellationToken cancellationToken) {
            /*
            var client = //_factory.Create(_terminal);
            var message = GetClientMessage();
            
            _eventHub.Publish($"Sending {_terminal.Name} {message}");
            var responseXml = await client.SendAsync(_transactionId, GetConfiguration(), message, _terminal, cancellationToken);
            _eventHub.Publish($"Received {_terminal.Name} {responseXml}");
            
            return ProcessResponseMessage(responseXml);
            */
            throw new Exception();
        }

        private string GetClientMessage() => Transformer.Transform(_requestXml, _terminal.Xslt!, _extensions);

        private Configuration GetConfiguration() {
            var configXml = Transformer.Transform($"<request name='{_requestName}'><config/></request>", _terminal.Xslt!, _extensions);
            var config = new Configuration();

            if (!string.IsNullOrWhiteSpace(configXml))
                config = DeSerialize<Configuration>(configXml) ?? new Configuration();

            config.Settings.AddRange(_terminal.Settings);
            
            var xsltSettings = config.Settings.Where(s => !_terminal.Settings.Any(t => t.Name.Equals(s.Name)));

            var newConfig = new Configuration { Settings = _terminal.Settings };
            newConfig.Settings.AddRange(xsltSettings);

            return newConfig;
        }

        private (string? message ,bool success) ProcessResponseMessage(string response) {
            var xml = WrapXmlInResponseTag(response)
                .Transform(_terminal.Xslt!, _extensions)
                .ToXml();

            var result = xml.FirstNode?.ToString(); 
            
            return (message: result, success: HasSucceeded(xml));
        }

        private string WrapXmlInResponseTag(string xmlStr) =>
            $"<request name='{_requestName}'>{xmlStr.ToXml()}</request>";

        private static bool HasSucceeded(XContainer xml) {
            if (xml.Nodes().Count() > 1)
                return Serializer.DeSerialize<Response>(xml.LastNode!.ToString())?.Success ?? true;

            return true;
        }
    }
}