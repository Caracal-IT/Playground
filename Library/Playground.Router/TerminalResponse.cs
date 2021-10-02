namespace Playground.Router;

[XmlRoot("terminal-response")]
public class TerminalResponse {
    [XmlAttribute("success")] public bool Success { get; set; } = true;
    [XmlAttribute("code")] public string Code { get; set; } = "00";
    [XmlAttribute("reference")] public string Reference { get; set; } = string.Empty;
}