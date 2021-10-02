namespace Playground.PaymentEngine.Application.UseCases.Withdrawals;

using Playground.Core.Model;

public record Withdrawal : Entity {
    public long CustomerId { get; set; }
    public decimal Amount { get; set; }
    public long WithdrawalStatusId { get; set; }
    public DateTime DateRequested { get; set; }
}