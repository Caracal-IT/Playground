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

        public Engine(TerminalStore store, ClientFactory factory) {
            _store = store;
            _factory = factory;
        }
        
        public async Task<string> ProcessAsync(Request request, CancellationToken token) {
            var terminals = await Task.WhenAll(request.Terminals.Select(GetTerminal));
            var response = string.Empty;

            foreach (var t in terminals)
                if (await TryProcessAsync(t)) break;
            
            return response;

            async Task<Terminal> GetTerminal(KeyValuePair<string, int> terminalSetting) {
                var (key, value) = terminalSetting;
                var terminal = await _store.GetTerminalAsync(key, token);
                terminal.RetryCount = value;
                return terminal;
            }

            async Task<bool> TryProcessAsync(Terminal terminal) {
                for (var i = 0; i < terminal.RetryCount; i++) {
                    if (await ProcessRequestAsync(terminal))
                        return true;

                    await Task.Delay(200, token);
                }

                return false;
            }
            
            async Task<bool> ProcessRequestAsync(Terminal terminal) {
                var message = Transformer.Transform(request.Data, terminal.OutXslt!);
                var client = _factory.Create(terminal.Name);
                response = await client.SendAsync(message);

                return true;
            }
        }
    }
}