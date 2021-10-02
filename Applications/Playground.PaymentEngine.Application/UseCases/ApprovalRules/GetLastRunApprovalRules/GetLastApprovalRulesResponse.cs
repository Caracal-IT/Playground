namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;

public record GetGetLastRunApprovalRulesResponse {
    public IEnumerable<ApprovalRuleHistory> Histories { get; set; } = Array.Empty<ApprovalRuleHistory>();
}