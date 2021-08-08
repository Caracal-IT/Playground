using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="transaction-types")]
    public class TransactionTypes {
        [XmlElement(ElementName="transaction-type")]
        public List<TransactionType> TransactionTypeList { get; set; }
    }
}