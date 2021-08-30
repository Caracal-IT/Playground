using System;
using Playground.PaymentEngine.Models.Shared;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Models.ApprovalRules {
    public class ApprovalRuleHistory {
        public long WithdrawalGroupId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<MetaData> Metadata { get; set; }
        public List<ApprovalRule> Rules { get; set; }
    }
}