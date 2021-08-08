using System.Collections.Generic;

namespace PaymentEngine.UseCases.Payments.Process {
    public class ProcessRequest {
        public List<long> Allocations { get; set; } = new();
    }
}