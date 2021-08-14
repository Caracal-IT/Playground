using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="accounts")]
    public class Accounts {
        [XmlElement(ElementName="account")]
        public List<Account> AccountList { get; set; }
    }
}