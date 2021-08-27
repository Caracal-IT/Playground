using AutoMapper;
using Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal;
using GetWithdrawals = Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;
using GetWithdrawal = Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal;
using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<ViewModel.CreateWithdrawalRequest, CreateWithdrawalRequest>();
            CreateMap<Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawals.Withdrawal, ViewModel.Withdrawal>();
            CreateMap<GetWithdrawal.Withdrawal, ViewModel.Withdrawal>();
        }
    }
}