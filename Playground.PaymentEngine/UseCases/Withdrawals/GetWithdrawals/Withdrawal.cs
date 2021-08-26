using System;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}