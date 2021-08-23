using System.Collections.Generic;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.AllocationStores.Model;
using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Stores.PaymentStores {
    public interface PaymentStore {
        Store GetStore();
        
        
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);
     
        
        
        IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds);
        IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id);
        IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds);
        WithdrawalGroup GetWithdrawalGroup(long id);
        
        
        IEnumerable<RuleHistory> GetRuleHistories(IEnumerable<long> withdrawalIds);

        void AddRuleHistories(IEnumerable<RuleHistory> histories);
    }
}