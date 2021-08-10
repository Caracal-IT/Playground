using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using PaymentEngine.Stores;
using PaymentEngine.Terminals.Clients;
using Router;

using static PaymentEngine.Helpers.Serializer;

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
        
        public async Task<string> ExecuteAsync(CallbackRequest request, CancellationToken token) {
            var terminalName = _paymentStore.GetStore()
                                            .Allocations
                                            .AllocationList
                                            .FirstOrDefault(a => a.Reference.Equals(request.Reference))
                                            ?.Terminal;

            if (terminalName == null) return "<XmlData/>";
            
            var req = new Request {
                RequestType = (int) RequestType.Callback,
                Data = $"<callback-request>{request.Data}</callback-request>", 
                Terminals = new Dictionary<string, int>{ { terminalName, 2 } }
            };

            var response = await _engine.ProcessAsync(req, token);
            var xml = response.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xml)) return string.Empty;

            var resp = DeSerialize<TerminalResponse>(xml);

            if (resp.IsSuccessfull && resp.Code == "00") {
                _paymentStore.GetStore().Allocations.AllocationList
                             .Where(a => a.Reference.Equals(request.Reference))
                             .ToList()
                             .ForEach(a => a.AllocationStatusId = 6);
            }
            
            return Serialize(resp);
        }
    }
}