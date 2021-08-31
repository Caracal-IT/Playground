using GetWithdrawalGroups = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using CreateWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using GroupWithdrawals = Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;
using GetWithdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;
using GetWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;
using AppendGroupWithdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals.AppendGroupWithdrawals;

using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<ViewModel.CreateWithdrawalRequest, CreateWithdrawal.CreateWithdrawalRequest>();
            CreateMap<CreateWithdrawal.Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawals.Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawal.Withdrawal, ViewModel.Withdrawal>();

            CreateMap<GetWithdrawalGroups.WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<GroupWithdrawals.WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<AppendGroupWithdrawals.WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
        }
    }
}