using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.ApprovalRules.Model {
    [XmlRoot(ElementName="approval-rule")]
    public class ApprovalRule {
        [XmlAttribute(AttributeName="name")]
        public string RuleName { get; set; } = "Rule1";
        [XmlAttribute(AttributeName="isSuccessful")]
        public bool IsSuccessful { get; set; }
        [XmlAttribute(AttributeName="message")]
        public string? Message { get; set; }
    }
}