using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Allocations.Model {
    [XmlRoot("allocation-status")]
    public class AllocationStatus {
        [XmlAttribute("id")]
        public long Id { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
    }
}