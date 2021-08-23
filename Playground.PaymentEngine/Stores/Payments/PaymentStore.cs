using System.Collections.Generic;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.Payments {
    public interface PaymentStore {
        Store GetStore();
        
        
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);
     
        
        
        IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds);
        IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id);
        IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds);
        WithdrawalGroup GetWithdrawalGroup(long id);
        
        
    }
}