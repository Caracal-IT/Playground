using System.Xml.Serialization;

namespace Playground.Router.Old {
    [XmlRoot("terminal-response")]
    public class Response {
        [XmlAttribute("success")]
        public bool Success { get; set; } = true;
    }
}