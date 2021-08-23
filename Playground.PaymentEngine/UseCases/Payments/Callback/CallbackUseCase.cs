using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Services.Routing;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.Stores.PaymentStores;
using Playground.Router;

namespace Playground.PaymentEngine.UseCases.Payments.Callback {
    public class CallbackUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly IRoutingService _routingService;

        public CallbackUseCase(PaymentStore paymentStore, IRoutingService routingService) {
            _paymentStore = paymentStore;
            _routingService = routingService;
        }
        
        public async Task<CallbackResponse> ExecuteAsync(CallbackRequest request, CancellationToken cancellationToken) {
            var transactionId = Guid.NewGuid();
            
            var allocations = _paymentStore.GetAllocationsByReference(request.Reference).ToList();
           
            if (!allocations.Any()) return new CallbackResponse();
            var terminals = new[] { allocations.First().Terminal };

            var req = new RoutingRequest(transactionId, request.Action, request.Data, terminals);

            var responses =  await _routingService.Send(req, cancellationToken);
            var response = responses.FirstOrDefault();
            
            if (response?.Result == null) return new CallbackResponse();
            
            if (response.TerminalResponse.Success) 
                allocations.ForEach(a => a.AllocationStatusId = 6);
            
            return new CallbackResponse{  Response = response.Result };
        }
    }
}