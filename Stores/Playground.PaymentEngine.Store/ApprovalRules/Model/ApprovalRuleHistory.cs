using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.ApprovalRules.Model {
    [XmlRoot(ElementName="approval-rule-history")]
    public class ApprovalRuleHistory {
        [XmlAttribute(AttributeName="withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute(AttributeName="transaction-id")]
        public Guid TransactionId { get; set; }
        [XmlAttribute(AttributeName="transaction-date")]
        public DateTime TransactionDate { get; set; }
        [XmlElement(ElementName="meta-data")]
        public List<MetaData> Metadata { get; set; }
        
        [XmlElement(ElementName="rule")]
        public List<ApprovalRule> Rules { get; set; }
    }
}