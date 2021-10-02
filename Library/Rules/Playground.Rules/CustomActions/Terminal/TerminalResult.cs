namespace Playground.Rules.CustomActions.Terminal;

using System.Xml.Serialization;

[XmlRoot("terminal-result")]
public class TerminalResult {
    [XmlAttribute("valid")]
    public bool IsValid { get; set; }
}