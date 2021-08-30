using Playground.PaymentEngine.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using CreateWithdrawal = Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal;
using GetWithdrawals = Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;
using GetWithdrawal = Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal;
using AppendGroupWithdrawals = Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals;

using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<ViewModel.CreateWithdrawalRequest, CreateWithdrawal.CreateWithdrawalRequest>();
            CreateMap<CreateWithdrawal.Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawals.Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawal.Withdrawal, ViewModel.Withdrawal>();

            CreateMap<UseCases.WithdrawalGroups.GroupWithdrawals.WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
            CreateMap<AppendGroupWithdrawals.WithdrawalGroup, ViewModel.WithdrawalGroup>()
                .ForMember(w => w.Withdrawals, s => s.MapFrom(w => w.WithdrawalIds));
        }
    }
}