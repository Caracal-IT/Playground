namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class ApprovalRuleOutcome {
        public long WithdrawalId { get; set; }
        public string RuleName { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}