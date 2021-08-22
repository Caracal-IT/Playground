using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="rule-history")]
    public class RuleHistory {
        [XmlAttribute(AttributeName="withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute(AttributeName="transaction-id")]
        public Guid TransactionId { get; set; }
        [XmlAttribute(AttributeName="transaction-date")]
        public DateTime TransactionDate { get; set; }
        [XmlElement(ElementName="meta-data")]
        public List<MetaData> Metadata { get; set; }
        
        [XmlElement(ElementName="rule")]
        public List<Rule> Rules { get; set; }
    }
}