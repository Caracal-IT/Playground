namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;

public record GetDepositResponse {
    public Deposit? Deposit { get; set; }
}