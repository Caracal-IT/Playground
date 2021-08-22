using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateRequest {
        public List<long> WithdrawalGroups { get; set; } = new();
    }
}