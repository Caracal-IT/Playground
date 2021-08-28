using Data = Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
        }
    }
}