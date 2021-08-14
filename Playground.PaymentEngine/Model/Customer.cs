using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="customer")]
    public class Customer {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="first-name")]
        public string FirstName { get; set; }
        [XmlAttribute(AttributeName="last-name")]
        public string LastName { get; set; }
        [XmlAttribute(AttributeName="balance", DataType="decimal")]
        public decimal Balance { get; set; }
    }
}