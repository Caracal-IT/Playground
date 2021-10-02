namespace Playground.PaymentEngine.Store.File.ApprovalRules;

using Playground.PaymentEngine.Store.ApprovalRules.Model;

[XmlRoot("repository")]
public class ApprovalRuleData {
    [XmlArray("approval-rule-history")] public List<ApprovalRuleHistory> ApprovalRuleRuleHistories { get; set; } = new();
}