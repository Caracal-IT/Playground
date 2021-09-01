using System;
using Playground.PaymentEngine.Models.Shared;

namespace Playground.PaymentEngine.Models.Deposits {
    public class Deposit {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DepositDate { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}