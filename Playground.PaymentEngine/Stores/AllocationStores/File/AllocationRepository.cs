using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.AllocationStores.Model;

namespace Playground.PaymentEngine.Stores.AllocationStores.File {
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