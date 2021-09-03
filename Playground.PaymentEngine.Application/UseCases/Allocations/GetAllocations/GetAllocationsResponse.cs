using System;

namespace Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations {
    public class GetAllocationsResponse {
        public IEnumerable<Allocation> Allocations { get; set; } = Array.Empty<Allocation>();
    }
}