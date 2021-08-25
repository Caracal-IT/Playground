using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules.File {
    [XmlRoot("repository")]
    public class ApprovalRuleData {
        [XmlArray("approval-rule-history")]
        public List<ApprovalRuleHistory> ApprovalRuleRuleHistories { get; set; } = new();
    }
}