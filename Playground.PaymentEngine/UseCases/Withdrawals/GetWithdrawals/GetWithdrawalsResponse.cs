using System;
using System.Collections.Generic;
using static System.Array;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals {
    public record Withdrawal(long Id, long CustomerId, decimal Amount, long WithdrawalStatusId, DateTime DateRequested);
    
    public record GetWithdrawalsResponse {
        public IEnumerable<Withdrawal> Withdrawals { get; set; } = Empty<Withdrawal>();
    }
}