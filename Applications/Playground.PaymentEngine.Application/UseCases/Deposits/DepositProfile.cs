namespace Playground.PaymentEngine.Application.UseCases.Deposits;

using Data = Playground.PaymentEngine.Store.Deposits.Model;

public class DepositProfile : Profile {
    public DepositProfile() {
        CreateMap<Data.Deposit, Deposit>();
    }
}