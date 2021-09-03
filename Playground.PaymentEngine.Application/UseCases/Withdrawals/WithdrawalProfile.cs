using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.Withdrawal, Withdrawal>();
        }
    }
}