namespace Playground.PaymentEngine.Application.UseCases.Withdrawals;

using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

public class WithdrawalProfile : Profile {
    public WithdrawalProfile() {
        CreateMap<Data.Withdrawal, Withdrawal>();
    }
}