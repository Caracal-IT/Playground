using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="customers")]
    public class Customers {
        [XmlElement(ElementName="customer")]
        public Customer Customer { get; set; }
    }
}