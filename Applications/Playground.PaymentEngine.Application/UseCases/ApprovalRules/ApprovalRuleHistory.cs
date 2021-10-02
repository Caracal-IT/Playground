namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules;

using Playground.Core.Model;
using Playground.PaymentEngine.Application.UseCases.Shared;

public record ApprovalRuleHistory: Entity {
    public long WithdrawalGroupId { get; set; }
    public Guid TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public List<MetaData> Metadata { get; set; } = new();
    public List<ApprovalRule> Rules { get; set; } = new();
}