using System.Xml.Serialization;

namespace Playground.PaymentEngine.Stores.CustomerStores.Model {
    [XmlRoot("customer")]
    public class Customer {
        [XmlAttribute("id")]
        public long Id { get; set; }
        
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }
        
        [XmlAttribute("last-name")]
        public string LastName { get; set; }
        
        [XmlAttribute("balance", DataType="decimal")]
        public decimal Balance { get; set; }
    }
}