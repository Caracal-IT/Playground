using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.Accounts;
using Playground.PaymentEngine.Stores.Allocations;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.Payments {
    public class FilePaymentStore: PaymentStore {
        private Store _store;
        private readonly AccountStore _accountStore;
        private readonly AllocationStore _allocationStore;

        public FilePaymentStore(AccountStore accountStore, AllocationStore allocationStore) {
            _accountStore = accountStore;
            _allocationStore = allocationStore;
            LoadStore();
        }

        public Store GetStore() => _store;


        public ExportAllocation GetExportAllocation(long allocationId) {
            var allocation = _allocationStore.GetAllocation(allocationId);
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
        
        

        private void LoadStore() {
            var path = Path.Join("Resources", "Data", "store.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            _store = (Store) new XmlSerializer(typeof(Store)).Deserialize(fileStream);
        }
    }
}