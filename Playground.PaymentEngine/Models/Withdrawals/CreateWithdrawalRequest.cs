namespace Playground.PaymentEngine.Models.Withdrawals {
    public record CreateWithdrawalRequest(long CustomerId, decimal Amount);
}