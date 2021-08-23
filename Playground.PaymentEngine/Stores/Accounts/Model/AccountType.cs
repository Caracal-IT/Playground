using System.Xml.Serialization;

namespace Playground.PaymentEngine.Stores.Accounts.Model {
    [XmlRoot(ElementName="account-type")]
    public class AccountType {
        [XmlAttribute("id")]
        public long Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("charge")]
        public string Charge { get; set; }
        [XmlAttribute("threshold", DataType="decimal")]
        public decimal Threshold { get; set; }
        [XmlAttribute("process-order")]
        public int ProcessOrder { get; set; }
    }
}