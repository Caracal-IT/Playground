using System.Xml.Serialization;

namespace Playground.Router {
    [XmlRoot("terminal-response")]
    public class Response {
        [XmlAttribute("success")]
        public bool Success { get; set; } = true;
    }
}