using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Withdrawals.Model {
    [XmlRoot("withdrawal-status")]
    public class WithdrawalStatus {
        [XmlAttribute("id")]
        public long Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}