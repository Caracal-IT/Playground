using System.Xml.Serialization;

namespace Playground.Router {
    [XmlRoot("response")]
    public class Response {
        [XmlElement("terminal-response")]
        public TerminalResponse TerminalResponse { get; set; } = new();
        [XmlElement("result")]
        public string Result { get; set; } = string.Empty;
    }
}