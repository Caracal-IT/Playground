using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.AllocationStores.Model;

namespace Playground.PaymentEngine.Stores.AllocationStores.File {
    public class FileAllocationStore: AllocationStore {
        private AllocationRepository _repository;
        private readonly ICacheService _cacheService;
        
        public FileAllocationStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _repository = GetRepository();
        }
        
        public Allocation GetAllocation(long id) =>
            _repository.Allocations
                       .FirstOrDefault(a => a.Id == id)
                       ??new Allocation();
        
        public IEnumerable<Allocation> GetAllocationsByReference(string reference) =>
            _repository.Allocations
                       .Where(a => !string.IsNullOrWhiteSpace(a.Terminal) && a.Reference.Equals(reference));
        
        public void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null) {
            var allocation = GetAllocation(id);
            allocation.AllocationStatusId = statusId;
            allocation.Terminal = terminal;
            allocation.Reference = reference;
        }
        
        private object _allocationLock = new object();

        public Allocation SaveAllocation(Allocation allocation) {
            var alloc = _repository.Allocations.FirstOrDefault(a => a.Id == allocation.Id);

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
        
        public void RemoveAllocations(long withdrawalGroupId) {
            var allocations = _repository.Allocations;
            
            _repository
                .Allocations
                .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                .ToList()
                .ForEach(a => allocations.Remove(a));
        }
        
        private static AllocationRepository GetRepository() {
            var path = Path.Join("Stores", "AccountStores", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (AllocationRepository) new XmlSerializer(typeof(AllocationRepository)).Deserialize(fileStream);
        }
    }
}