using AutoMapper;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;
using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Withdrawal, ViewModel.Withdrawal>();
        }
    }
}