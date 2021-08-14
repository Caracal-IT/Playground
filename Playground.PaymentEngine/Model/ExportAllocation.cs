using System.Collections.Generic;

namespace Playground.PaymentEngine.Model {
    public class ExportAllocation {
        public long CustomerId { get; set; }
        public long AccountId { get; set; }
        public long AllocationId { get; set; }
        public long AccountTypeId { get; set; }
        public decimal Amount { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}