namespace Playground.PaymentEngine.Store.Withdrawals.Model;

[XmlRoot("withdrawal-status")]
public class WithdrawalStatus : Entity {
    [XmlAttribute("name")] public string Name { get; set; } = string.Empty;
}