using System.Xml.Serialization;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    [XmlRoot("rule-input")]
    public class RuleInput {
        [XmlAttribute("withdrawal-id")]
        public long WithdrawalId { get; set; }
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute("balance")]
        public decimal Balance { get; set; }
        [XmlAttribute("amount")]
        public decimal Amount { get; set; }
    }
}