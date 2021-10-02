namespace Playground.PaymentEngine.Api.Models.Withdrawals;

public record CreateWithdrawalRequest(long CustomerId, decimal Amount);