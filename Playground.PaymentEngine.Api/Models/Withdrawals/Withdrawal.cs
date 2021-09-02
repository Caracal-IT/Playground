using System;

namespace Playground.PaymentEngine.Api.Models.Withdrawals {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
}