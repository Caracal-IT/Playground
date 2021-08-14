using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Playground.PaymentEngine.Stores;
using Playground.Router;

using static Playground.Xml.Serialization.Serializer;

namespace Playground.PaymentEngine.UseCases.Payments.Callback {
    public class CallbackUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly Engine _engine;
        private readonly TerminalStore _terminalStore;
        
        public CallbackUseCase(PaymentStore paymentStore, Engine engine, TerminalStore terminalStore) {
            _paymentStore = paymentStore;
            _engine = engine;
            _terminalStore = terminalStore;
        }
        
        public async Task<CallbackResponse> ExecuteAsync(CallbackRequest request, CancellationToken token) {
            var allocations = _paymentStore.GetAllocationsByReference(request.Reference).ToList();
           
            if (!allocations.Any()) return new CallbackResponse();

            var req2 = new Request<string> {
                Name = request.Action,
                Payload = request.Data,
                Terminals = new []{ allocations.First().Terminal }
            };
            
            var response =  await _engine.ProcessAsync(req2, token);
            var xml = response.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xml)) return new CallbackResponse();

            var xDoc = XDocument.Parse(xml);
            
            var resp = DeSerialize<TerminalResponse>(xDoc.Root!.FirstNode!.ToString());

            if (resp.IsSuccessfull && resp.Code == "00") 
                allocations.ForEach(a => a.AllocationStatusId = 6);
            
            return new CallbackResponse{ TerminalResponse = resp, Response = xDoc.Root!.LastNode!.ToString() };
        }
    }
}