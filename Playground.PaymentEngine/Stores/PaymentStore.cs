using System.Collections.Generic;
using Playground.PaymentEngine.Model;

namespace Playground.PaymentEngine.Stores {
    public interface PaymentStore {
        Store GetStore();

        Account GetAccount(long id);
        Allocation GetAllocation(long id);
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);
        IEnumerable<Allocation> GetAllocationsByReference(string reference);
        IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
        IEnumerable<Terminal> GetTerminals();
        IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds);
        IEnumerable<RuleHistory> GetRuleHistories(IEnumerable<long> withdrawalIds);

        void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null);

        void LogTerminalResults(IEnumerable<TerminalResult> results);

        void AddRuleHistories(IEnumerable<RuleHistory> histories);
    }
}