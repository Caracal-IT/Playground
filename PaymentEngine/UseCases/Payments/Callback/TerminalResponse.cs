using System.Xml.Serialization;

namespace PaymentEngine.UseCases.Payments.Callback {
    [XmlRoot("terminal-response")]
    public class TerminalResponse {
        [XmlAttribute("success")]
        public bool IsSuccessfull { get; set; }
        [XmlAttribute("reference-number")]
        public string Reference { get; set; }
        [XmlAttribute("return-code")]
        public string Code { get; set; }
    }
}