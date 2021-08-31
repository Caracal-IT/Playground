using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.ApprovalRules.Model {
    [XmlRoot("approval-rule")]
    public class ApprovalRule {
        [XmlAttribute("name")]
        public string RuleName { get; set; } = "Rule1";
        [XmlAttribute("isSuccessful")]
        public bool IsSuccessful { get; set; }
        [XmlAttribute("message")]
        public string? Message { get; set; }
    }
}