using System;
using System.Xml.Serialization;
using Playground.Core;
using Playground.Core.Data;

namespace Playground.PaymentEngine.Store.Withdrawals.Model {
    [XmlRoot("withdrawal")]
    public class Withdrawal: Entity {
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute("amount", DataType="decimal")]
        public decimal Amount { get; set; }
        [XmlAttribute("date-requested", DataType="date")]
        public DateTime DateRequested { get; set; }
        [XmlAttribute("withdrawal-status-id")]
        public long WithdrawalStatusId { get; set; }
        
        [XmlIgnore]
        public bool IsDeleted { get; set; }
    }
}