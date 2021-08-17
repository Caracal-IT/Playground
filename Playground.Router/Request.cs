using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.Router {
    [XmlRoot("request")]
    public class Request {
        [XmlIgnore] public Guid TransactionId { get; set; } = Guid.NewGuid();
        [XmlAttribute("name")] public string Name { get; set; } = "request";
        [XmlElement("payload")] public string Payload { get; set; } = string.Empty;
        [XmlIgnore] public IEnumerable<Terminal> Terminals { get; set; } = Array.Empty<Terminal>();
        [XmlIgnore] public Dictionary<string, object> Extensions { get; set; } = new();
    }
}