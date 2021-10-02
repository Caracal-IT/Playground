namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;

public record GetWithdrawalGroupsResponse {
    public IEnumerable<WithdrawalGroup> WithdrawalGroups { get; set; } = Array.Empty<WithdrawalGroup>();
}