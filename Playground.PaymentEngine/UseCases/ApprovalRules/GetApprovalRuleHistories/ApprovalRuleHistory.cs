using System;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.Stores.Model;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class ApprovalRuleHistory {
        public long WithdrawalGroupId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<MetaData> Metadata { get; set; }
        public List<ApprovalRule> Rules { get; set; }
    }
}