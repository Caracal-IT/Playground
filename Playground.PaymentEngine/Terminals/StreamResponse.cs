using System.Xml.Serialization;

namespace Playground.PaymentEngine.Terminals {
    [XmlRoot("response")]
    public class StreamResponse {
        [XmlElement("name")]
        public string Name { get; set; }
        
        [XmlAttribute("reference")]
        public string Reference { get; set; }
        
        [XmlElement("code")]
        public string Code { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("file-name")]
        public string FileName { get; set; }
    }
}