namespace Playground.PaymentEngine.Api.Models.Withdrawals {
    public record WithdrawalGroup {
        public long Id { get; init; }
        public long CustomerId { get; init; }
        public List<long> Withdrawals { get; init; }
    }
}