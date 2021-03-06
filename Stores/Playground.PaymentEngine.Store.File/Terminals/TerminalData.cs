namespace Playground.PaymentEngine.Store.File.Terminals;

using Playground.PaymentEngine.Store.Terminals.Model;

[XmlRoot("repository")]
public class TerminalData {
    [XmlArray("terminals")]
    [XmlArrayItem("terminal")]
    public List<Terminal> Terminals { get; set; } = new();

    [XmlArray("terminal-maps")]
    [XmlArrayItem("terminal-map")]
    public List<TerminalMap> TerminalMaps { get; set; } = new();

    [XmlArray("terminal-results")]
    [XmlArrayItem("terminal-result")]
    public List<TerminalResult> TerminalResults { get; set; } = new();
}