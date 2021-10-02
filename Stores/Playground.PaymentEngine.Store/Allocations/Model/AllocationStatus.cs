namespace Playground.PaymentEngine.Store.Allocations.Model;

[XmlRoot("allocation-status")]
public class AllocationStatus : Entity {
    [XmlAttribute("name")] public string Name { get; set; } = string.Empty;
}