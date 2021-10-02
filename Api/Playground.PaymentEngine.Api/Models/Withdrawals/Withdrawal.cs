namespace Playground.PaymentEngine.Api.Models.Withdrawals;

public record Withdrawal: Entity {
    public long CustomerId { get; init; }
    public decimal Amount { get; init; }
    public long WithdrawalStatusId { get; init; }
    public DateTime DateRequested { get; init; }
}