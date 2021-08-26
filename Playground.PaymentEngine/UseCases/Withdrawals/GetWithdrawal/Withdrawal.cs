using System;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}