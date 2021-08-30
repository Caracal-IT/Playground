using System.Xml.Serialization;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.RunApprovalRules {
    [XmlRoot("rule-input")]
    public class RuleInput {
        [XmlAttribute("withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute("balance")]
        public decimal Balance { get; set; }
        [XmlAttribute("amount")]
        public decimal Amount { get; set; }
    }
}