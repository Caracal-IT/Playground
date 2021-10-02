namespace Playground.PaymentEngine.Store.File.Deposits;

using Playground.PaymentEngine.Store.Deposits.Model;

[XmlRoot("repository")]
public class DepositData {
    [XmlArray("deposits")]
    [XmlArrayItem("deposit")]
    public List<Deposit> Deposits { get; set; } = new();
}