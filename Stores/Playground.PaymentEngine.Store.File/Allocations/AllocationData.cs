namespace Playground.PaymentEngine.Store.File.Allocations;
    
using Playground.PaymentEngine.Store.Allocations.Model;

[XmlRoot("repository")]
public class AllocationData {
    [XmlArray("allocations")]
    [XmlArrayItem("allocation")]
    public List<Allocation> Allocations { get; set; } = new();

    [XmlArray("allocation-statuses")]
    [XmlArrayItem("allocation-status")]
    public List<AllocationStatus> AllocationStatuses { get; set; } = new();
}