namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;

public record GroupWithdrawalsResponse {
    public IEnumerable<WithdrawalGroup> WithdrawalGroups { get; set; } = Array.Empty<WithdrawalGroup>();
}