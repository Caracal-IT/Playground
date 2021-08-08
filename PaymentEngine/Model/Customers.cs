using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="customers")]
    public class Customers {
        [XmlElement(ElementName="customer")]
        public List<Customer> CustomerList { get; set; }
    }
}