using System;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="transaction")]
    public class Transaction {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="withdrawal-id")]
        public long WithdrawalId { get; set; }
        [XmlAttribute(AttributeName="amount", DataType="decimal")]
        public decimal Amount { get; set; }
        [XmlAttribute(AttributeName="transaction-type-id")]
        public long TransactionTypeId { get; set; }
        [XmlAttribute(AttributeName="transaction-date", DataType="date")]
        public DateTime TransactionDate { get; set; }
    }
}