using System.Xml.Serialization;

namespace Playground.PaymentEngine.Stores.Withdrawals.Model {
    [XmlRoot(ElementName="withdrawal-status")]
    public class WithdrawalStatus {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
    }
}