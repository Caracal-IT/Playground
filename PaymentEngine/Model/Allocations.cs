using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="allocations")]
    public class Allocations {
        [XmlElement(ElementName="allocation")]
        public List<Allocation> AllocationList { get; set; }
    }
}