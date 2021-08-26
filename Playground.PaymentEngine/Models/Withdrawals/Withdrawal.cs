using System;

namespace Playground.PaymentEngine.Models.Withdrawals {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}