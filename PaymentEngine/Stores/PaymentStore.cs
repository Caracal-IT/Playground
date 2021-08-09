using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using PaymentEngine.Model;

namespace PaymentEngine.Stores {
    public interface PaymentStore {
        Store GetStore();

        Account GetAccount(long id);
        Allocation GetAllocation(long id);
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);
        void SetAllocationStatus(long id, long statusId);
        IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId);
    }
}