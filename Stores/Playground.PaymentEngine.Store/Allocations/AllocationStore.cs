using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Store.Allocations {
    public interface AllocationStore {
        Task<IEnumerable<Allocation>> GetAllocationsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Allocation>> GetAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken);
        Task DeleteAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken);
        Task<IEnumerable<Allocation>> GetAllocationsByReferenceAsync(string reference, CancellationToken cancellationToken);
        
        Task SetAllocationStatusAsync(long id, long statusId, CancellationToken cancellationToken);
        Task SetAllocationStatusAsync(long id, long statusId, string terminal, CancellationToken cancellationToken);
        Task SetAllocationStatusAsync(long id, long statusId, string terminal, string? reference, CancellationToken cancellationToken);
            
        Task<Allocation> SaveAllocationAsync(Allocation allocation, CancellationToken cancellationToken);

        Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken);
    }
}