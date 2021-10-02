namespace Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocation;

public class GetAllocationUseCase {
    private readonly AllocationStore _store;
    private readonly IMapper _mapper;
    
    public GetAllocationUseCase(AllocationStore store, IMapper mapper) {
        _store = store;
        _mapper = mapper;
    }

    public async Task<GetAllocationResponse> ExecuteAsync(long id, CancellationToken cancellationToken) {
        var allocations = await _store.GetAllocationsAsync(new []{id}, cancellationToken);
        return new GetAllocationResponse { Allocation = _mapper.Map<Allocation>(allocations.FirstOrDefault()) };
    }
}