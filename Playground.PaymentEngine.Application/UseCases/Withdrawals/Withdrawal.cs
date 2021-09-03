using System;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}