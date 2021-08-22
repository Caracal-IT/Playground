using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Services.CacheService;

namespace Playground.PaymentEngine.Stores {
    public class FilePaymentStore: PaymentStore {
        private Store _store;
        private readonly ICacheService _cacheService;

        public FilePaymentStore(ICacheService cacheService) {
            _cacheService = cacheService;
            LoadStore();
        }

        public Store GetStore() => _store;

        public Account GetAccount(long id) =>
            _store.Accounts.FirstOrDefault(a => a.Id == id) ?? new Account();
        
        public Allocation GetAllocation(long id) =>
            _store.Allocations
                .FirstOrDefault(a => a.Id == id)
                  ??new Allocation();

        public Customer GetCustomer(long id) =>
            _store.Customers.FirstOrDefault(c => c.Id == id);

        public IEnumerable<Account> GetCustomerAccounts(long id) =>
            _store.Accounts.Where(a => a.CustomerId == id);

        public IEnumerable<AccountType> GetAccountTypes(IEnumerable<long> accountTypeIds) =>
            _store.AccountTypes.Where(t => accountTypeIds.Contains(t.Id));
        
        public ExportAllocation GetExportAllocation(long allocationId) {
            var allocation = GetAllocation(allocationId);
            var account = GetAccount(allocation.AccountId);

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

        public IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId) =>
            _cacheService.GetValue($"{nameof(GetTerminals)}_{accountTypeId}", () => 
                _store.TerminalMaps
                      .Join(_store.Terminals,
                          tm => tm.TerminalId,
                          t => t.Id,
                          (tm, t) => new { Map = tm, Terminal = t }
                      )
                      .Where(t => t.Map.Enabled && t.Map.AccountTypeId == accountTypeId)
                      .OrderBy(t => t.Map.Order)
                      .Select(t => t.Terminal)
                      .ToList()
            );

        public IEnumerable<Terminal> GetTerminals() =>
            _cacheService.GetValue(nameof(GetTerminals), () => _store.Terminals);

        public IEnumerable<RuleHistory> GetRuleHistories(IEnumerable<long> withdrawalGroupIds) =>
            _store.RuleHistories
                  .Where(h => withdrawalGroupIds.Contains(h.WithdrawalGroupId));

        public void LogTerminalResults(IEnumerable<TerminalResult> results) =>
            _store.TerminalResults.AddRange(results);

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