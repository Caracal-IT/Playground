using System;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.Stores.Model;

namespace Playground.PaymentEngine.Models.ApprovalRules {
    public class ApprovalRuleHistory {
        public long WithdrawalGroupId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<MetaData> Metadata { get; set; }
        public List<ApprovalRule> Rules { get; set; }
    }
}