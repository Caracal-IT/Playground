using System;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits {
    public record GetDepositsResponse {
        public IEnumerable<Deposit> Deposits { get; set; } = Array.Empty<Deposit>();
    }
}