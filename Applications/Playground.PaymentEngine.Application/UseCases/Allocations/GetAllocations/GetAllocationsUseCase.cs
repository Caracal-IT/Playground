namespace Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations;

public class GetAllocationsUseCase {
    private readonly AllocationStore _store;
    private readonly IMapper _mapper;
    
    public GetAllocationsUseCase(AllocationStore store, IMapper mapper) {
        _store = store;
        _mapper = mapper;
    }

    public async Task<GetAllocationsResponse> ExecuteAsync(CancellationToken cancellationToken) {
        var allocations = await _store.GetAllocationsAsync(cancellationToken);
        return new GetAllocationsResponse { Allocations = _mapper.Map<IEnumerable<Allocation>>(allocations) };
    }
}