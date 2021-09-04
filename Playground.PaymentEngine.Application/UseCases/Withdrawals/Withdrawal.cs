using System;
using Playground.Core.Model;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals {
    public record Withdrawal: Entity {
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
        public long WithdrawalStatusId { get; set; }
        public DateTime DateRequested { get; set; }
    }
}