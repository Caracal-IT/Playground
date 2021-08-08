using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="withdrawals")]
    public class Withdrawals {
        [XmlElement(ElementName="withdrawal")]
        public List<Withdrawal> WithdrawalList { get; set; }
    }
}