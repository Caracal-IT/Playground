using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Deposits.Model {
    [XmlRoot(ElementName="allocation")]
    public class Deposit {
        [XmlAttribute("id")]
        public long Id { get; set; }
        
        [XmlAttribute("account-id")]
        public long AccountId { get; set; }
        [XmlAttribute("amount", DataType="decimal")]
        public decimal Amount { get; set; }
    }
}