namespace Playground.PaymentEngine.Store.Deposits.Model;

[XmlRoot(ElementName = "allocation")]
public class Deposit : Entity {
    [XmlAttribute("account-id")] public long AccountId { get; set; }

    [XmlAttribute("amount", DataType = "decimal")]
    public decimal Amount { get; set; }

    [XmlAttribute("deposit-date", DataType = "date")]
    public DateTime DepositDate { get; set; }

    [XmlElement("meta-data")] public List<MetaData> MetaData { get; set; } = new();
}