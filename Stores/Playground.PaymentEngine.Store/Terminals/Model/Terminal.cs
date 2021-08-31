using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.Terminals.Model {
    [XmlRoot(ElementName="terminal")]
    public class Terminal {
        [XmlAttribute("id")]
        public long Id { get; set; }
        
        [XmlAttribute("name")]
        public string? Name { get; set; }
        
        [XmlAttribute("type")]
        public string? Type { get; set; }
        
        [XmlAttribute("retry-count")]
        public int RetryCount { get; set; }
        
        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public List<Setting> Settings { get; set; } = new();
    }
}