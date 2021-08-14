using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.Router {
    [XmlRoot("request")]
    public class Request<T> where T : class {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlElement("payload")]
        public T? Payload { get; set; }
        
        [XmlIgnore]
        public int RequestType { get; set; } 
        
        [XmlIgnore]
        public Dictionary<string, int> Terminals { get; set; } = new Dictionary<string, int>();
    }
}