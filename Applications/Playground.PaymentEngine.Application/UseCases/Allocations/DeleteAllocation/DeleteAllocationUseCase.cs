namespace Playground.PaymentEngine.Application.UseCases.Allocations.DeleteAllocation {
    public class DeleteAllocationUseCase {
        private readonly AllocationStore _store;

        public DeleteAllocationUseCase(AllocationStore store) => _store = store;

        public async Task ExecuteAsync(long id, CancellationToken cancellationToken) => 
            await _store.DeleteAllocationsAsync(new []{id}, cancellationToken);
    }
}