using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;

namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateUseCase {
        private readonly PaymentStore _store;

        public AutoAllocateUseCase(PaymentStore store) => 
            _store = store;

        public async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            await Task.Delay(0, cancellationToken);
            request.WithdrawalGroups.ForEach(RemoveAllocations);

            return new AutoAllocateResponse();
        }

        private void RemoveAllocations(long withdrawalGroupId) {
            var allocations = _store.GetStore().Allocations.AllocationList;
            
            _store.GetStore()
                .Allocations
                .AllocationList
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));
        }
    }
}