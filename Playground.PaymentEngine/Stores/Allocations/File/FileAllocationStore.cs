using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Allocations.Model;

namespace Playground.PaymentEngine.Stores.Allocations.File {
    public class FileAllocationStore: AllocationStore {
        private readonly AllocationRepository _repository;

        private readonly object _allocationLock = new();
        
        public FileAllocationStore() => 
            _repository = GetRepository();

        public Task<Allocation> GetAllocationAsync(long id, CancellationToken cancellationToken) {
            var result = _repository.Allocations
                                    .FirstOrDefault(a => a.Id == id)
                                    ??new Allocation();
            
            return Task.FromResult(result);
        }

        public Task<IEnumerable<Allocation>> GetAllocationsByReferenceAsync(string reference, CancellationToken cancellationToken) {
            var result = _repository.Allocations
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
                    allocation.Id = _repository.Allocations.LastOrDefault()?.Id ?? 0 + 1;
                }
            }
            else
                _repository.Allocations.Remove(alloc);

            _repository.Allocations.Add(allocation);

            return allocation;
        }

        public Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            var allocations = _repository.Allocations;
            
            _repository
                .Allocations
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));

            return Task.CompletedTask;
        }
        
        private static AllocationRepository GetRepository() {
            var path = Path.Join("Stores", "Allocations", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (AllocationRepository) new XmlSerializer(typeof(AllocationRepository)).Deserialize(fileStream);
        }
    }
}