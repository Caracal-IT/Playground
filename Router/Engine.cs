using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Router.Clients;
using XsltTransform;

namespace Router {
    public class Engine: RouterEngine {
        private readonly TerminalStore _store;
        private readonly ClientFactory _factory;
        private readonly Dictionary<string, object> _extensions;

        public Engine(TerminalStore store, TerminalExtensions extensions, ClientFactory factory) {
            _store = store;
            _factory = factory;
            _extensions = extensions.GetExtensions();
        }

        public async Task<List<string>> ExportDataAsync(Request request, List<string> preferredTerminals, CancellationToken token) {
            var terminals = await Task.WhenAll(request.Terminals.Select(i => GetTerminal(i, token)));
            var terminal = terminals.FirstOrDefault(t => preferredTerminals.Contains(t.Name)) ?? terminals.FirstOrDefault();

            return terminal == null 
                ? new List<string>() 
                : new List<string> { Transformer.Transform(request.Data, terminal.Xslt!, _extensions) };
        }
        
        public async Task<List<string>> ProcessAsync(Request request, CancellationToken token) {
            var terminals = await Task.WhenAll(request.Terminals.Select(i => GetTerminal(i, token)));
            var response = new List<string>();

            foreach (var t in terminals)
                if (await TryProcessAsync(t)) break;
            
            return response;

            async Task<bool> TryProcessAsync(Terminal terminal) {
                for (var i = 0; i < terminal.RetryCount; i++) {
                    if (await ProcessRequestAsync(terminal))
                        return true;

                    await Task.Delay(200, token);
                }

                return false;
            }
            
            async Task<bool> ProcessRequestAsync(Terminal terminal) {
                var message = Transformer.Transform(request.Data, terminal.Xslt!, _extensions);
                var client = _factory.Create(terminal.Name);
                var resp = await client.SendAsync(message, request.RequestType);
                response.Add(Transformer.Transform(resp, terminal.Xslt!, _extensions));

                return true;
            }
        }

        private async Task<Terminal> GetTerminal(KeyValuePair<string, int> terminalSetting, CancellationToken token) {
            var (key, value) = terminalSetting;
            var terminal = await _store.GetTerminalAsync(key, token);
            terminal.RetryCount = value;
            
            return terminal;
        }
    }
}