using System;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}