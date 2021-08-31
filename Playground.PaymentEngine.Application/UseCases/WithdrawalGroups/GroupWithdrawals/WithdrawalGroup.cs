namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}