using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="account-type")]
    public class AccountType {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName="charge")]
        public string Charge { get; set; }
        [XmlAttribute(AttributeName="threshold", DataType="decimal")]
        public decimal Threshold { get; set; }
    }
}