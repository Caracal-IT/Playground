using System;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="withdrawal")]
    public class Withdrawal {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute(AttributeName="amount", DataType="decimal")]
        public decimal Amount { get; set; }
        [XmlAttribute(AttributeName="balance", DataType="decimal")]
        public decimal Balance { get; set; }
        [XmlAttribute(AttributeName="date-requested", DataType="date")]
        public DateTime DateRequested { get; set; }
        [XmlAttribute(AttributeName="withdrawal-status-id")]
        public long WithdrawalStatusId { get; set; }
    }
}