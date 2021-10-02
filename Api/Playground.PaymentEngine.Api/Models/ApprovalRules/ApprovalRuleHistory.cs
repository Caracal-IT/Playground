namespace Playground.PaymentEngine.Api.Models.ApprovalRules;

public record ApprovalRuleHistory: Entity {
    public long WithdrawalGroupId { get; set; }
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public List<MetaData> Metadata { get; set; } = new();
    public List<ApprovalRule> Rules { get; set; } = new();
}