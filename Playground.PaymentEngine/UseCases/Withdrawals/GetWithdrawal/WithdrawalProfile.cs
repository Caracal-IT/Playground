using AutoMapper;
using Data = Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.Withdrawal, Withdrawal>();
        }
    }
}