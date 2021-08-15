using System.Xml.Serialization;

namespace Playground.PaymentEngine.UseCases.Payments.Callback {
    [XmlRoot("terminal-response")]
    public class TerminalResponse {
        [XmlAttribute("success")]
        public bool IsSuccessful { get; set; }
        [XmlAttribute("reference")]
        public string Reference { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
    }
}