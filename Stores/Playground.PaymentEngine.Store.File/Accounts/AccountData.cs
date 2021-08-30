using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Accounts.Model;

namespace Playground.PaymentEngine.Store.File.Accounts {
    [XmlRoot("repository")]
    public class AccountData {
        [XmlArray("accounts")]
        [XmlArrayItem("account")]
        public List<Account> Accounts { get; set; }
		
        [XmlArray("account-types")]
        [XmlArrayItem("account-type")]
        public List<AccountType> AccountTypes { get; set; }
    }
}