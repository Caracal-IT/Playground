namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;

public record GetWithdrawalResponse {
    public Withdrawal? Withdrawal { get; set; }
}