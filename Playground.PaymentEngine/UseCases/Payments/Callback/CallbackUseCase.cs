using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Playground.PaymentEngine.Stores;
using Playground.Router;
using Playground.Router.Old;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.PaymentEngine.UseCases.Payments.Callback {
    public class CallbackUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly Engine _engine;

        public CallbackUseCase(PaymentStore paymentStore, Engine engine) {
            _paymentStore = paymentStore;
            _engine = engine;
        }
        
        public async Task<CallbackResponse> ExecuteAsync(CallbackRequest request, CancellationToken token) {
            var transactionId = Guid.NewGuid();
            
            var allocations = _paymentStore.GetAllocationsByReference(request.Reference).ToList();
           
            if (!allocations.Any()) return new CallbackResponse();

            var req2 = new Request<string> {
                Name = request.Action,
                Payload = request.Data,
                Terminals = new []{ allocations.First().Terminal }
            };
            
            var response =  await _engine.ProcessAsync(transactionId, req2, token);
            var xml = response.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(xml)) return new CallbackResponse();

            var xDoc = XDocument.Parse(xml);
            
            var resp = DeSerialize<TerminalResponse>(xDoc.Root!.FirstNode!.ToString());

            if (resp!.IsSuccessful && resp.Code == "00") 
                allocations.ForEach(a => a.AllocationStatusId = 6);
            
            return new CallbackResponse{ TerminalResponse = resp, Response = xDoc.Root!.LastNode!.ToString() };
        }
    }
}