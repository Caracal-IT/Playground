using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Extensions;
using Playground.PaymentEngine.Stores.Terminals;
using Playground.PaymentEngine.Terminals.Functions;
using Playground.Router;

namespace Playground.PaymentEngine.Services.Routing {
    public class RoutingService: IRoutingService {
        private readonly TerminalStore _terminalStore;
        
        private readonly Engine _engine;
        private readonly Dictionary<string, object> _extensions;

        private IEnumerable<Terminal> _terminals;

        public RoutingService(TerminalStore terminalStore, Engine engine, XsltExtensions extensions) {
            _engine = engine;
            _terminalStore = terminalStore;
            _extensions = extensions.GetExtensions();
        }

        public async Task<IEnumerable<Response>> SendAsync(RoutingRequest request, CancellationToken cancellationToken) {
            await InitTerminals(cancellationToken);
            
            var req = new Request { 
                TransactionId = request.TransactionId,
                Name = request.Name,
                Payload = request.Payload,
                Extensions = _extensions,
                Terminals = _terminals.Where(t => request.Terminals.Contains(t.Name))
            };

            return await _engine.ProcessAsync(req, cancellationToken);
        }

        private async Task InitTerminals(CancellationToken cancellationToken) {
            if (_terminals != null)
                return;
            
            var terminalEnum = await _terminalStore.GetTerminalsAsync(cancellationToken);
            var terminals = terminalEnum.Select(async t => new Terminal {
                Name = t.Name,
                Settings = t.Settings.Select(s => new Setting(s.Name, s.Value)),
                Type = t.Type,
                RetryCount = t.RetryCount,
                Xslt = await GetXslt(t.Name)
            });

            _terminals = await Task.WhenAll(terminals);

            async Task<string> GetXslt(string name) {
                var path = Path.Join("Terminals", "Templates", $"{name}.xslt");

                if (!File.Exists(path)) return string.Empty;

                return await path.ReadFromFileAsync(cancellationToken);
            }   
        }
    }
}