using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="allocation-statuses")]
    public class AllocationStatuses {
        [XmlElement(ElementName="allocation-status")]
        public List<AllocationStatus> AllocationStatusList { get; set; }
    }
}