namespace Playground.PaymentEngine.Api.Models.Withdrawals;

public record GroupWithdrawalRequest {
    public List<long> Withdrawals { get; set; } = new();
}