using AutoMapper;

using Data = Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.Withdrawal, Withdrawal>();
        }
    }
}