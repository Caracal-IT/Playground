using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.ApprovalRules.Model {
    [XmlRoot("approval-rule-history")]
    public class ApprovalRuleHistory {
        [XmlAttribute("withdrawal-group-id")]
        public long WithdrawalGroupId { get; set; }
        [XmlAttribute("transaction-id")]
        public Guid TransactionId { get; set; }
        [XmlAttribute("transaction-date")]
        public DateTime TransactionDate { get; set; }

        [XmlElement("meta-data")]
        public List<MetaData> Metadata { get; set; } = new();
        
        [XmlElement("rule")]
        public List<ApprovalRule> Rules { get; set; } = new();
    }
}