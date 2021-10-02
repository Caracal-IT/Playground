namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;

public record CreateWithdrawalRequest(long CustomerId, decimal Amount);