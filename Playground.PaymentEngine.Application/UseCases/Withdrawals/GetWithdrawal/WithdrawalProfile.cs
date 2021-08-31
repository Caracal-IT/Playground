using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<Data.Withdrawal, Withdrawal>();
        }
    }
}