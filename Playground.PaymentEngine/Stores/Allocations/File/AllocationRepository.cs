using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Allocations.Model;

namespace Playground.PaymentEngine.Stores.Allocations.File {
    [XmlRoot("repository")]
    public class AllocationRepository {
        [XmlArray("allocations")]
        [XmlArrayItem("allocation")]
        public List<Allocation> Allocations { get; set; }
		
        [XmlArray("allocation-statuses")]
        [XmlArrayItem("allocation-status")]
        public List<AllocationStatus> AllocationStatuses { get; set; }
    }
}