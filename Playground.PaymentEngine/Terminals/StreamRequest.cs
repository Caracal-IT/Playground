using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;

namespace Playground.PaymentEngine.Terminals {
    [XmlRoot("request")]
    public class StreamRequest {
        [XmlElement("reference")]
        public string Reference { get; set; }
        
        [XmlElement("code")]
        public string Code { get; set; }
        
        [XmlElement("amount")]
        public decimal Amount { get; set; }
        
        [XmlArray("meta-data")]
        [XmlArrayItem("meta-data-item")]
        public List<MetaData> MetaData { get; set; }
    }
}