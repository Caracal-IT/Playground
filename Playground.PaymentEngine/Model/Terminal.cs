using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="terminal")]
    public class Terminal {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName="type")]
        public string Type { get; set; }
        [XmlAttribute(AttributeName="retry-count")]
        public int RetryCount { get; set; }
        
        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public List<Setting> Settings { get; set; } = new();
    }
}