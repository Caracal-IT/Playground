namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups;

using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

public class WithdrawalProfile : Profile {
    public WithdrawalProfile() {
        CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
    }
}