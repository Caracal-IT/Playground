using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.Customers.Model {
    [XmlRoot("customer")]
    public class Customer {
        [XmlAttribute("id")]
        public long Id { get; set; }
        
        [XmlAttribute("first-name")]
        public string? FirstName { get; set; }
        
        [XmlAttribute("last-name")]
        public string? LastName { get; set; }
        
        [XmlAttribute("balance", DataType="decimal")]
        public decimal Balance { get; set; }
        
        [XmlElement("meta-data")]
        public List<MetaData> MetaData { get; set; } = new();
    }
}