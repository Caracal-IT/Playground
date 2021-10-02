namespace Playground.PaymentEngine.Store.Terminals.Model;

[XmlRoot("terminal-map")]
public class TerminalMap : Entity {
    [XmlAttribute("account-type-id")] public long AccountTypeId { get; set; }

    [XmlAttribute("terminal-id")] public long TerminalId { get; set; }

    [XmlAttribute("order")] public short Order { get; set; }

    [XmlAttribute("enabled")] public bool Enabled { get; set; }
}