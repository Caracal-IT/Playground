using System;
using Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit {
    public record CreateDepositRequest(long AccountId, decimal Amount, DateTime DepositDate) {
        public List<MetaData> MetaData { get; set; } = new();
    }
}