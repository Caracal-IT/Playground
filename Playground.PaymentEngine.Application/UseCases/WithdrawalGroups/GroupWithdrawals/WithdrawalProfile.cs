using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
        }
    }
}