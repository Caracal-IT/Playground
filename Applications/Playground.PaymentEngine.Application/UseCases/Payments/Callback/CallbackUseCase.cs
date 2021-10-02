namespace Playground.PaymentEngine.Application.UseCases.Payments.Callback;

using Router.Services;

public class CallbackUseCase {
    private readonly AllocationStore _allocationStore;
    private readonly IRoutingService _routingService;

    public CallbackUseCase(AllocationStore allocationStore, IRoutingService routingService) {
        _routingService = routingService;
        _allocationStore = allocationStore;
    }

    public async Task<CallbackResponse> ExecuteAsync(CallbackRequest request, CancellationToken cancellationToken) {
        var transactionId = Guid.NewGuid();

        var allocationEnum = await _allocationStore.GetAllocationsByReferenceAsync(request.Reference, cancellationToken);
        var allocations = allocationEnum.ToList();

        if (!allocations.Any()) return new CallbackResponse();
        var terminals = new[] { allocations.First().Terminal };

        var req = new RoutingRequest(transactionId, request.Action, request.Data, terminals!);

        var responses = await _routingService.SendAsync(req, cancellationToken);
        var response = responses.FirstOrDefault();

        if (response?.Result == null) return new CallbackResponse();

        if (response.TerminalResponse.Success)
            allocations.ForEach(a => a.AllocationStatusId = 6);

        return new CallbackResponse { Response = response.Result };
    }
}