namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}