using System.Xml.Serialization;

namespace Playground.PaymentEngine.Terminals {
    [XmlRoot("request")]
    public class StreamRequest {
        [XmlElement("reference")]
        public string Reference { get; set; }
        
        [XmlElement("code")]
        public string Code { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlElement("card-holder")]
        public string CardHolder { get; set; }
        
        [XmlElement("hash")]
        public string Hash { get; set; }
    }
}