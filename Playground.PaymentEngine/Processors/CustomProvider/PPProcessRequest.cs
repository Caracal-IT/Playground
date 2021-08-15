using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Processors.CustomProvider {
    [XmlRoot("request")]
    public class PPProcessRequest {
        [XmlAttribute("reference")]
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
        
        [XmlElement("code")]
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [XmlElement("amount")]
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}