using System.Collections.Generic;
using Playground.PaymentEngine.Stores.Allocations.Model;

namespace Playground.PaymentEngine.Stores.Allocations {
    public interface AllocationStore {
        Allocation GetAllocation(long id);
        IEnumerable<Allocation> GetAllocationsByReference(string reference);
        
        void SetAllocationStatus(long id, long statusId, string terminal = null, string reference = null);
        Allocation SaveAllocation(Allocation allocation);

        void RemoveAllocations(long withdrawalGroupId);
        
        ExportAllocation GetExportAllocation(long allocationId);
        IEnumerable<ExportAllocation> GetExportAllocations(IEnumerable<long> allocationIds);

    }
}