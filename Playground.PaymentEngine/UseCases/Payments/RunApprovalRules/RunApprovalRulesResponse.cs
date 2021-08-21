using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesResponse {
        public List<ApprovalRuleOutcome> Outcomes { get; set; } = new();
    }
}