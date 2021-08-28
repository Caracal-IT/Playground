namespace Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}