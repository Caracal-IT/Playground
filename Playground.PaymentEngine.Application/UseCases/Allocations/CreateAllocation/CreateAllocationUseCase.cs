using Data = Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation {
    public class CreateAllocationUseCase {
        private readonly AllocationStore _store;
        private readonly IMapper _mapper;
        
        public CreateAllocationUseCase(AllocationStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<CreateAllocationResponse> ExecuteAsync(CreateAllocationRequest request, CancellationToken cancellationToken) {
            var allocation = _mapper.Map<Data.Allocation>(request);
            var response = await  _store.SaveAllocationAsync(allocation, cancellationToken);
            
            return new CreateAllocationResponse{ Allocation = _mapper.Map<Allocation>(response) };
        }
    }
}