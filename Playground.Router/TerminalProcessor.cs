using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Playground.Router.Clients;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.Router {
    internal class TerminalProcessor<T> where T : class {
        private readonly TerminalStore _store;
        internal readonly ClientFactory Factory;
        internal readonly Dictionary<string, object> Extensions;
        internal readonly Request<T> Request;
        private readonly CancellationToken _cancellationToken;

        private readonly List<string> _response = new();
        public string RequestXml;

        private TerminalProcessor(
            Request<T> request,
            TerminalStore store,
            TerminalExtensions extensions,
            ClientFactory factory,
            CancellationToken cancellationToken) {
            _store = store;
            Factory = factory;
            Extensions = extensions.GetExtensions();
            Request = request;
            _cancellationToken = cancellationToken;
        }

        public static async Task<List<string>> ProcessAsync<TS>(
            Request<TS> request,
            TerminalStore store,
            TerminalExtensions extensions,
            ClientFactory factory,
            CancellationToken cancellationToken) where TS : class {
            var p = new TerminalProcessor<TS>(request, store, extensions, factory, cancellationToken);
            return await p.ProcessAsync();
        }

        private async Task<List<string>> ProcessAsync() {
            RequestXml = SerializeRequest();
            var terminals = await Task.WhenAll(Request.Terminals.Select(GetTerminal));

            foreach (var t in terminals)
                if (await TryProcessAsync(t))
                    break;

            return _response;
        }

        private string SerializeRequest() {
            var reqStr = Serialize(Request);
            var reqXml = XDocument.Parse(reqStr);

            if (!Request.Payload!.ToString()!.StartsWith("<")) return reqXml.ToString();

            var n = reqXml.XPathSelectElement("request/payload");
            n!.RemoveAll();
            n.Add(XDocument.Parse(Request.Payload.ToString()!).Root!);

            return reqXml.ToString();
        }

        private async Task<bool> TryProcessAsync(Terminal terminal) {
            for (var i = 0; i < terminal.RetryCount; i++) {
                if (await TryProcessRequestAsync(terminal))
                    return true;

                await Task.Delay(200, _cancellationToken);
            }

            return false;
        }
        
        private async Task<bool> TryProcessRequestAsync(Terminal terminal) {
            try {
                var response = await Transaction<T>.ProcessAsync(this, terminal);

                if (string.IsNullOrWhiteSpace(response))
                    return false;
                
                _response.Add(response);
                
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        private async Task<Terminal> GetTerminal(string key) =>
            await _store.GetTerminalAsync(key, _cancellationToken);
    }
}