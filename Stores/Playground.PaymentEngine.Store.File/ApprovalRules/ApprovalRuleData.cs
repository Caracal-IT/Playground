using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Store.ApprovalRules.Model;

namespace Playground.PaymentEngine.Store.File.ApprovalRules {
    [XmlRoot("repository")]
    public class ApprovalRuleData {
        [XmlArray("approval-rule-history")]
        public List<ApprovalRuleHistory> ApprovalRuleRuleHistories { get; set; } = new();
    }
}