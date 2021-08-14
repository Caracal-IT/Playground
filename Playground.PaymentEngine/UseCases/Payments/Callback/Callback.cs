using System.Xml.Serialization;

namespace Playground.PaymentEngine.UseCases.Payments.Callback {
    [XmlRoot("callback")]
    public class Callback {
        [XmlAttribute("reference")]
        public string Reference { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}