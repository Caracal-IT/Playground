using System.Collections.Generic;

namespace Playground.PaymentEngine.Application.UseCases.Payments.Process {
    public record ProcessRequest {
        public bool Consolidate { get; set; } = false;
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<long> Allocations { get; set; } = new();
    }
}