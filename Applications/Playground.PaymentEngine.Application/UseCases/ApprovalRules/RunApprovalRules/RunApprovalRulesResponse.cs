namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;

public record RunApprovalRulesResponse {
    public List<ApprovalRuleOutcome> Outcomes { get; set; } = new();
}