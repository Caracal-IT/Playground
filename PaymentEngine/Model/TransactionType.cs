using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="transaction-type")]
    public class TransactionType {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
    }
}