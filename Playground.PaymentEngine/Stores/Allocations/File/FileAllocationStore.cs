using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Accounts;
using Playground.PaymentEngine.Stores.Allocations.Model;

namespace Playground.PaymentEngine.Stores.Allocations.File {
    public class FileAllocationStore: AllocationStore {
        private AllocationRepository _repository;
        private AccountStore _accountStore;
        
        public FileAllocationStore(AccountStore accountStore) {
            _accountStore = accountStore;
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
        
        public ExportAllocation GetExportAllocation(long allocationId) {
            var allocation = GetAllocation(allocationId);
            var account = _accountStore.GetAccount(allocation.AccountId);

            return new ExportAllocation {
                AllocationId = allocation.Id,
                Amount = allocation.Amount + allocation.Charge,
                AccountId = account.Id,
                AccountTypeId = account.AccountTypeId,
                CustomerId = account.CustomerId,
                MetaData = account.MetaData
            };
        }

        public IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds) =>
            allocationIds.Select(GetExportAllocation)
                .Where(a => a.AccountId > 0);
        
        private static AllocationRepository GetRepository() {
            var path = Path.Join("Stores", "Allocations", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (AllocationRepository) new XmlSerializer(typeof(AllocationRepository)).Deserialize(fileStream);
        }
    }
}