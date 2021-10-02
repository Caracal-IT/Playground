namespace Playground.PaymentEngine.Api.Setup.Profiles;

using WithdrawalGroups = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups;
using ViewModel = Models.Withdrawals;

public class WithdrawalGroupProfile: Profile {
    public WithdrawalGroupProfile() {
        CreateMap<WithdrawalGroups.WithdrawalGroup, ViewModel.WithdrawalGroup>()
            .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
    }
}