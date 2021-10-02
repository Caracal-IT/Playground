namespace Playground.PaymentEngine.Api.Models.ApprovalRules;

public record ApprovalRuleOutcome: Entity {
    public long WithdrawalGroupId { get; set; }
    public string RuleName { get; set; } = string.Empty;
    public bool IsSuccessful { get; set; }
    public string Message { get; set; } = string.Empty;
}