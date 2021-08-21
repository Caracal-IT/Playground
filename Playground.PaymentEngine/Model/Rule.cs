using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="rule")]
    public class Rule {
        [XmlAttribute(AttributeName="name")]
        public string RuleName { get; set; }
        [XmlAttribute(AttributeName="isSuccessful")]
        public bool IsSuccessful { get; set; }
        [XmlAttribute(AttributeName="message")]
        public string Message { get; set; }
    }
}