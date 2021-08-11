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
            var allocations = _paymentStore.GetAllocationsByReference(request.Reference).ToList();
           
            if (!allocations.Any()) return "<not-found/>";
            
            var req = new Request {
                RequestType = (int) RequestType.Callback,
                Data = $"<callback-request>{request.Data}</callback-request>", 
                Terminals = new Dictionary<string, int>{ { allocations.First().Terminal, 2 } }
            };

            var response = await _engine.ProcessAsync(req, token);
            var xml = response.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xml)) return "<not-found/>";

            var resp = DeSerialize<TerminalResponse>(xml);

            if (resp.IsSuccessfull && resp.Code == "00") 
                allocations.ForEach(a => a.AllocationStatusId = 6);
            
            return Serialize(resp);
        }
    }
}