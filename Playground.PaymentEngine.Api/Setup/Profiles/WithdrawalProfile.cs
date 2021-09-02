using Playground.PaymentEngine.Api.Models.Withdrawals;
using GetWithdrawalGroups = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using CreateWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using GroupWithdrawals = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;
using GetWithdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;
using GetWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;
using AppendGroupWithdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals.AppendGroupWithdrawals;

using ViewModel = Playground.PaymentEngine.Api.Models.Withdrawals;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<CreateWithdrawalRequest, CreateWithdrawal.CreateWithdrawalRequest>();
            CreateMap<CreateWithdrawal.Withdrawal, Withdrawal>();
            CreateMap<GetWithdrawals.Withdrawal, Withdrawal>();
            CreateMap<GetWithdrawal.Withdrawal, Withdrawal>();

            CreateMap<GetWithdrawalGroups.WithdrawalGroup, WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<GroupWithdrawals.WithdrawalGroup, WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<AppendGroupWithdrawals.WithdrawalGroup, WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
        }
    }
}