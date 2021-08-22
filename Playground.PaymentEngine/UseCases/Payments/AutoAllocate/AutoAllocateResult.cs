namespace Playground.PaymentEngine.UseCases.Payments.AutoAllocate {
    public class AutoAllocateResult {
        public long WithdrawalGroupId { get; set; }
        public decimal Amount { get; set; }
        public long AccountId { get; set; }
    }
}