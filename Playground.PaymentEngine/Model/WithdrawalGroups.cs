using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="withdrawal-groups")]
    public class WithdrawalGroups {
        [XmlElement(ElementName="withdrawal-group")]
        public List<WithdrawalGroup> WithdrawalGroupList { get; set; }
    }
}