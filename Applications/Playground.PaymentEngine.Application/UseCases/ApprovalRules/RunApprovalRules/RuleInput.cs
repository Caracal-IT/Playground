namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;

using System.Xml.Serialization;

[XmlRoot("rule-input")]
public record RuleInput {
    [XmlAttribute("withdrawal-group-id")]
    public long WithdrawalGroupId { get; set; }
    [XmlAttribute("customer-id")]
    public long CustomerId { get; set; }
    [XmlAttribute("balance")]
    public decimal Balance { get; set; }
    [XmlAttribute("amount")]
    public decimal Amount { get; set; }
}