using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Allocations.Model {
    [XmlRoot("allocation")]
    public class Allocation {
        [XmlAttribute("id")]
        public long Id { get; set; }
        [XmlAttribute("withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute("account-id")]
        public long AccountId { get; set; }
        [XmlAttribute("amount", DataType="decimal")]
        public decimal Amount { get; set; }
        [XmlAttribute("charge", DataType="decimal")]
        public decimal Charge { get; set; }
        [XmlAttribute("allocation-status-id")]
        public long AllocationStatusId { get; set; }
        
        [XmlAttribute("terminal")]
        public string? Terminal { get; set; }
        
        [XmlAttribute("reference")]
        public string? Reference { get; set; }
    }
}