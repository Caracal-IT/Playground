using System.Collections.Generic;

namespace PaymentEngine.UseCases.Payments.Process {
    public class ProcessRequest {
        public bool Consolidate { get; set; } = false;
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<long> Allocations { get; set; } = new();
    }
}