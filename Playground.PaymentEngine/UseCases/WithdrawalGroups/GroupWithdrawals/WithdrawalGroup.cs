using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}