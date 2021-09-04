using System.Collections.Generic;
using static System.Array;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals {
    public record GetWithdrawalsResponse {
        public IEnumerable<Withdrawal> Withdrawals { get; set; } = Empty<Withdrawal>();
    }
}