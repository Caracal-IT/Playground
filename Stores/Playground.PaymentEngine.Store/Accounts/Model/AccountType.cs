using System.Xml.Serialization;
using Playground.Core;
using Playground.Core.Data;

namespace Playground.PaymentEngine.Store.Accounts.Model {
    [XmlRoot(ElementName="account-type")]
    public class AccountType: Entity {
        [XmlAttribute("name")]
        public string? Name { get; set; }
        [XmlAttribute("charge")]
        public decimal Charge { get; set; }
        [XmlAttribute("threshold", DataType="decimal")]
        public decimal Threshold { get; set; }
        [XmlAttribute("process-order")]
        public int ProcessOrder { get; set; }
    }
}