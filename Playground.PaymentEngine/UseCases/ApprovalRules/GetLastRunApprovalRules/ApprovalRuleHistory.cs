using System;
using Playground.PaymentEngine.Store.ApprovalRules.Model;
using Playground.PaymentEngine.UseCases.Shared;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.GetLastRunApprovalRules {
    public class ApprovalRuleHistory {
        public long WithdrawalGroupId { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public List<MetaData> Metadata { get; set; }
        public List<ApprovalRule> Rules { get; set; }
    }
}