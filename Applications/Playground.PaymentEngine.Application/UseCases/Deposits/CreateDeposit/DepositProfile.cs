namespace Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;

using Data = Playground.PaymentEngine.Store.Deposits.Model;

public class DepositProfile: Profile {
    public DepositProfile() {
        CreateMap<CreateDepositRequest, Data.Deposit>();
    }
}