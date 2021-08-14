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
        void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null);
        IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
    }
}