namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RuleInput {
        public long WithdrawalId { get; set; }
        public long CustomerId { get; set; }
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
    }
}