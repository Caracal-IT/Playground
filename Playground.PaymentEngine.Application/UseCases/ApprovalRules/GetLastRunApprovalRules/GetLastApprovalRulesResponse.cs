using System;

namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules {
    public class GetGetLastRunApprovalRulesResponse {
        public IEnumerable<ApprovalRuleHistory> Histories { get; set; } = Array.Empty<ApprovalRuleHistory>();
    }
}