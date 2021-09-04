using System.Xml.Serialization;

namespace Playground.Rules.CustomActions.Terminal {
    [XmlRoot("terminal-result")]
    public class TerminalResult {
        [XmlAttribute("valid")]
        public bool IsValid { get; set; }
    }
}