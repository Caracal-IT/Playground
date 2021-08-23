using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;

namespace Playground.PaymentEngine.Stores.AccountStores.File {
    [XmlRoot("repository")]
    public class AccountRepository {
        [XmlArray("accounts")]
        [XmlArrayItem("account")]
        public List<Account> Accounts { get; set; }
		
        [XmlArray("account-types")]
        [XmlArrayItem("account-type")]
        public List<AccountType> AccountTypes { get; set; }
    }
}