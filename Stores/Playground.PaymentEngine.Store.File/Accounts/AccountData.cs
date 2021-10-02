namespace Playground.PaymentEngine.Store.File.Accounts;

using Playground.PaymentEngine.Store.Accounts.Model;

[XmlRoot("repository")]
public class AccountData {
    [XmlArray("accounts")]
    [XmlArrayItem("account")]
    public List<Account> Accounts { get; set; } = new();

    [XmlArray("account-types")]
    [XmlArrayItem("account-type")]
    public List<AccountType> AccountTypes { get; set; } = new();
}