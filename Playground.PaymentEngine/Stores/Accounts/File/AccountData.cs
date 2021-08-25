using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Accounts.Model;

namespace Playground.PaymentEngine.Stores.Accounts.File {
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