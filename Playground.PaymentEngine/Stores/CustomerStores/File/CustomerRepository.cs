using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.CustomerStores.Model;

namespace Playground.PaymentEngine.Stores.CustomerStores.File {
    [XmlRoot("repository")]
    public class CustomerRepository {
        [XmlArray("customers")]
        [XmlArrayItem("customer")]
        public List<Customer> Customers { get; set; }
    }
}