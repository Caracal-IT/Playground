namespace Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal {
    public record CreateWithdrawalRequest(long CustomerId, decimal Amount);
}