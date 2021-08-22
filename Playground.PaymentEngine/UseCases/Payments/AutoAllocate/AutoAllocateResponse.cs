using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateResponse {
        public List<AutoAllocateResult> AllocationResults { get; set; } = new();
    }
}