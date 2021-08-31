using System;

namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class GetApprovalRuleHistoriesResponse {
        public IEnumerable<ApprovalRuleHistory> Histories { get; set; } = Array.Empty<ApprovalRuleHistory>();
    }
}