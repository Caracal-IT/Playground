using Playground.PaymentEngine.Api.Models.Withdrawals;
using WithdrawalGroups = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups;
using ViewModel = Playground.PaymentEngine.Api.Models.Withdrawals;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class WithdrawalGroupProfile: Profile {
        public WithdrawalGroupProfile() {
            CreateMap<WithdrawalGroups.WithdrawalGroup, WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
        }
    }
}