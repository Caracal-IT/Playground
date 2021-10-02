namespace Playground.PaymentEngine.Store.File.Customers;

using Playground.PaymentEngine.Store.Customers.Model;

[XmlRoot("repository")]
public class CustomerData {
    [XmlArray("customers")]
    [XmlArrayItem("customer")]
    public List<Customer> Customers { get; set; } = new();
}