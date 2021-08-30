namespace Playground.PaymentEngine.Models.ApprovalRules {
    public class ApprovalRuleOutcome {
        public long WithdrawalGroupId { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}