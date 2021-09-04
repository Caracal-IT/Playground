namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal {
    public record CreateWithdrawalResponse {
        public Withdrawal? Withdrawal { get; set; }
    }
}