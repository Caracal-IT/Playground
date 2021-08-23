using System.Collections.Generic;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Stores.PaymentStores {
    public interface PaymentStore {
        Store GetStore();

        
        Allocation GetAllocation(long id);
        
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);
        IEnumerable<Allocation> GetAllocationsByReference(string reference);
       // IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
       // IEnumerable<Terminal> GetTerminals();
        IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds);
        IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id);
        IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds);
        WithdrawalGroup GetWithdrawalGroup(long id);
        IEnumerable<RuleHistory> GetRuleHistories(IEnumerable<long> withdrawalIds);

        void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null);

        void AddRuleHistories(IEnumerable<RuleHistory> histories);

        Allocation SaveAllocation(Allocation allocation);
    }
}