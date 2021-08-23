using System.Collections.Generic;
using Playground.PaymentEngine.Stores.Model;

namespace Playground.PaymentEngine.Stores.Allocations.Model {
    public class ExportAllocation {
        public long CustomerId { get; set; }
        public long AccountId { get; set; }
        public long AllocationId { get; set; }
        public long AccountTypeId { get; set; }
        public decimal Amount { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}