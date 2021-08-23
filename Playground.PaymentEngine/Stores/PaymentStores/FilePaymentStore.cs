using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.AccountStores;
using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Stores.PaymentStores {
    public class FilePaymentStore: PaymentStore {
        private Store _store;
        private readonly ICacheService _cacheService;
        private readonly AccountStore _accountStore;

        public FilePaymentStore(ICacheService cacheService, AccountStore accountStore) {
            _cacheService = cacheService;
            _accountStore = accountStore;
            LoadStore();
        }

        public Store GetStore() => _store;

        public Allocation GetAllocation(long id) =>
            _store.Allocations
                .FirstOrDefault(a => a.Id == id)
                  ??new Allocation();
        
        
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

        public IEnumerable<Allocation> GetAllocationsByReference(string reference) =>
            _store.Allocations
                  .Where(a => !string.IsNullOrWhiteSpace(a.Terminal) && a.Reference.Equals(reference));

        public IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds) =>
            _store.Withdrawals
                  .Where(w => withdrawalIds.Contains(w.Id));
        
        public IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id) {
            var group = _store.WithdrawalGroups.FirstOrDefault(g => g.Id == id)??new WithdrawalGroup();
            return GetWithdrawals(group.WithdrawalIds);
        }
        public IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds) => 
            _store.WithdrawalGroups
                  .Where(g => withdrawalGroupIds.Contains(g.Id));
        
        public WithdrawalGroup GetWithdrawalGroup(long id) =>
            _store.WithdrawalGroups
                  .FirstOrDefault(g => g.Id == id);

        public void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null) {
            var allocation = GetAllocation(id);
            allocation.AllocationStatusId = statusId;
            allocation.Terminal = terminal;
            allocation.Reference = reference;
        }

        public IEnumerable<RuleHistory> GetRuleHistories(IEnumerable<long> withdrawalGroupIds) =>
            _store.RuleHistories
                  .Where(h => withdrawalGroupIds.Contains(h.WithdrawalGroupId));

        public void AddRuleHistories(IEnumerable<RuleHistory> histories) =>
            _store.RuleHistories.AddRange(histories);

        private object _allocationLock = new object();

        public Allocation SaveAllocation(Allocation allocation) {
            var alloc = _store.Allocations.FirstOrDefault(a => a.Id == allocation.Id);

            if (alloc == null) {
                lock (_allocationLock) {
                    allocation.Id = _store.Allocations.LastOrDefault()?.Id ?? 0 + 1;
                }
            }
            else
                _store.Allocations.Remove(alloc);

            _store.Allocations.Add(allocation);

            return allocation;
        }

        private void LoadStore() {
            var path = Path.Join("Resources", "Data", "store.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            _store = (Store) new XmlSerializer(typeof(Store)).Deserialize(fileStream);
        }
    }
}