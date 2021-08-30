using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Allocations;
using Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Store.File.Allocations {
    public class FileAllocationStore: FileStore, AllocationStore {
        private readonly AllocationData _data;

        private readonly object _allocationLock = new();
        
        public FileAllocationStore() => 
            _data = GetRepository<AllocationData>();

        public Task<Allocation> GetAllocationAsync(long id, CancellationToken cancellationToken) {
            var result = _data.Allocations
                                    .FirstOrDefault(a => a.Id == id)
                                    ??new Allocation();
            
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Allocation>> GetAllocationsByReferenceAsync(string reference, CancellationToken cancellationToken) {
            var result = _data.Allocations
                                    .Where(a => !string.IsNullOrWhiteSpace(a.Terminal) && a.Reference.Equals(reference));

            return Task.FromResult(result);
        }

        public Task SetAllocationStatusAsync(long id, long statusId, CancellationToken cancellationToken) =>
            SetAllocationStatusAsync(id, statusId, null, cancellationToken);
        
        public Task SetAllocationStatusAsync(long id, long statusId, string terminal, CancellationToken cancellationToken) =>
            SetAllocationStatusAsync(id, statusId, terminal, null, cancellationToken);

        public async Task SetAllocationStatusAsync(long id, long statusId, string terminal, string reference, CancellationToken cancellationToken) {
            var allocation = await GetAllocationAsync(id, cancellationToken);
            allocation.AllocationStatusId = statusId;
            allocation.Terminal = terminal;
            allocation.Reference = reference;
        }

        public async Task<Allocation> SaveAllocationAsync(Allocation allocation, CancellationToken cancellationToken) {
            var alloc = await GetAllocationAsync(allocation.Id, cancellationToken);

            if (alloc == null) {
                lock (_allocationLock) {
                    allocation.Id = _data.Allocations.LastOrDefault()?.Id ?? 0 + 1;
                }
            }
            else
                _data.Allocations.Remove(alloc);

            _data.Allocations.Add(allocation);

            return allocation;
        }

        public Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            var allocations = _data.Allocations;
            
            _data
                .Allocations
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));

            return Task.CompletedTask;
        }
    }
}