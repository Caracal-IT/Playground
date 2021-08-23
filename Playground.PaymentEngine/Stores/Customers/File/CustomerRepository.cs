using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers.File {
    [XmlRoot("repository")]
    public class CustomerRepository {
        [XmlArray("customers")]
        [XmlArrayItem("customer")]
        public List<Customer> Customers { get; set; }
    }
}