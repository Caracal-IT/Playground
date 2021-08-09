using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PaymentEngine.Stores;
using Router;

namespace PaymentEngine.UseCases.Payments.Callback {
    public class CallbackUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly RouterEngine _engine;
        private readonly TerminalStore _terminalStore;
        
        public CallbackUseCase(PaymentStore paymentStore, RouterEngine engine, TerminalStore terminalStore) {
            _paymentStore = paymentStore;
            _engine = engine;
            _terminalStore = terminalStore;
        }
        
        public async Task<string> ExecuteAsync(string xml, CancellationToken token) {
            var terminalDef = _paymentStore.GetStore().Terminals.TerminalList.First();
            var terminal = await _terminalStore.GetTerminalAsync(terminalDef.Name, token);
            
            var req = new Request {
                Data = xml, 
                Terminals = new Dictionary<string, int>{ { "Terminal1", 2 } }
            };

            var response = await _engine.ProcessAsync(req, token);
            return response.FirstOrDefault()??string.Empty;
        }
    }
}