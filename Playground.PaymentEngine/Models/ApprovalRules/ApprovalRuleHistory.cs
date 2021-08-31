using System;
using Playground.PaymentEngine.Models.Shared;

namespace Playground.PaymentEngine.Models.ApprovalRules {
    public class ApprovalRuleHistory {
        public long WithdrawalGroupId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<MetaData> Metadata { get; set; } = new();
        public List<ApprovalRule> Rules { get; set; } = new();
    }
}