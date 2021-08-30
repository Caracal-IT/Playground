using System;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.GetLastRunApprovalRules {
    public class GetGetLastRunApprovalRulesResponse {
        public IEnumerable<ApprovalRuleHistory> Histories { get; set; } = Array.Empty<ApprovalRuleHistory>();
    }
}