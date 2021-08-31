using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Store.File.Allocations {
    [XmlRoot("repository")]
    public class AllocationData {
        [XmlArray("allocations")]
        [XmlArrayItem("allocation")]
        public List<Allocation> Allocations { get; set; } = new();

        [XmlArray("allocation-statuses")]
        [XmlArrayItem("allocation-status")]
        public List<AllocationStatus> AllocationStatuses { get; set; } = new();
    }
}