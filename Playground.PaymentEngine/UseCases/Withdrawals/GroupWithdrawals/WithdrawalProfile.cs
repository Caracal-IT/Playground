using AutoMapper;
using Data = Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
        }
    }
}