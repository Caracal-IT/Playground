using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="account-types")]
    public class AccountTypes {
        [XmlElement(ElementName="account-type")]
        public List<AccountType> AccountTypeList { get; set; }
    }
}