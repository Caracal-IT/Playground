namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.AppendGroupWithdrawals {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}