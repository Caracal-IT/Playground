namespace Playground.PaymentEngine.Store.Customers.Model;

[XmlRoot("customer")]
public class Customer : Entity {
    [XmlAttribute("balance", DataType = "decimal")]
    public decimal Balance { get; set; }

    [XmlElement("meta-data")] public List<MetaData> MetaData { get; set; } = new();
}