namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;

public record GetApprovalRuleHistoriesResponse {
    public IEnumerable<ApprovalRuleHistory> Histories { get; set; } = Array.Empty<ApprovalRuleHistory>();
}