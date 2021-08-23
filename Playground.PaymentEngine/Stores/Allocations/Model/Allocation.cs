using System.Xml.Serialization;

namespace Playground.PaymentEngine.Stores.Allocations.Model {
    [XmlRoot(ElementName="allocation")]
    public class Allocation {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute(AttributeName="account-id")]
        public long AccountId { get; set; }
        [XmlAttribute(AttributeName="amount", DataType="decimal")]
        public decimal Amount { get; set; }
        [XmlAttribute(AttributeName="charge", DataType="decimal")]
        public decimal Charge { get; set; }
        [XmlAttribute(AttributeName="allocation-status-id")]
        public long AllocationStatusId { get; set; }
        
        [XmlAttribute(AttributeName="terminal")]
        public string Terminal { get; set; }
        
        [XmlAttribute(AttributeName="reference")]
        public string Reference { get; set; }
    }
}