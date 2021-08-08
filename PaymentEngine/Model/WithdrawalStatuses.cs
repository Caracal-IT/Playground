using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="withdrawal-statuses")]
    public class WithdrawalStatuses {
        [XmlElement(ElementName="withdrawal-status")]
        public List<WithdrawalStatus> WithdrawalStatusList { get; set; }
    }
}