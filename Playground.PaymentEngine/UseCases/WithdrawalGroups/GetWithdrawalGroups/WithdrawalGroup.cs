namespace Playground.PaymentEngine.UseCases.WithdrawalGroups.GetWithdrawalGroups {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}