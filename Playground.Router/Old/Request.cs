using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.Router.Old {
    [XmlRoot("request")]
    public class Request<T> where T : class {
        [XmlAttribute("name")] public string Name { get; set; } = "request";

        [XmlElement("payload")]
        public T? Payload { get; set; }
        
        [XmlIgnore]
        public int RequestType { get; set; } 
        
        [XmlIgnore]
        public IEnumerable<string> Terminals { get; set; } = new List<string>();

        [XmlIgnore] private TerminalExtensions Extensions { get; set; }
    }
}