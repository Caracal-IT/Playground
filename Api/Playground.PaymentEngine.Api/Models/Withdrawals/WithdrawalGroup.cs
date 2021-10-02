namespace Playground.PaymentEngine.Api.Models.Withdrawals;

public record WithdrawalGroup: Entity {
    public long CustomerId { get; init; }
    public List<long> Withdrawals { get; init; }
}