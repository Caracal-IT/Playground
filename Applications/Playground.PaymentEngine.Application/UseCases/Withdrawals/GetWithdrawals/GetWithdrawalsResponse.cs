namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;

using static System.Array;

public record GetWithdrawalsResponse {
    public IQueryable<Withdrawal> Withdrawals { get; set; } = Empty<Withdrawal>().AsQueryable();
}