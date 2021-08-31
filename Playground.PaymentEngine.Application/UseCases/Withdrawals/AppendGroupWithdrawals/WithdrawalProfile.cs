using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.AppendGroupWithdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
        }
    }
}