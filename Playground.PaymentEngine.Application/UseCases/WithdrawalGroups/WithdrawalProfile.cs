using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.WithdrawalGroup, WithdrawalGroup>();
        }
    }
}