using System;
using Playground.PaymentEngine.Models.Shared;

namespace Playground.PaymentEngine.Models.Deposits {
    public record CreateDepositRequest(long AccountId, decimal Amount, DateTime DepositDate) {
        public List<MetaData> MetaData { get; set; } = new();
    }
}