using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Store.File.Customers {
    [XmlRoot("repository")]
    public class CustomerData {
        [XmlArray("customers")]
        [XmlArrayItem("customer")]
        public List<Customer> Customers { get; set; } = new();
    }
}