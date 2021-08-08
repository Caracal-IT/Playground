using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="transactions")]
    public class Transactions {
        [XmlElement(ElementName="transaction")]
        public List<Transaction> TransactionList { get; set; }
    }
}