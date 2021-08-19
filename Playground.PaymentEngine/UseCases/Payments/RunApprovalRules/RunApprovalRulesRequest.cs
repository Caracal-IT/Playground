using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesRequest {
        public List<long> Withdrawals { get; set; } = new();
    }
}