namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawalGroups {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}