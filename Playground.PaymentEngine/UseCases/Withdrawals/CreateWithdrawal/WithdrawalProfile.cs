using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

namespace Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<CreateWithdrawalRequest, Data.Withdrawal>();
            CreateMap<Data.Withdrawal, Withdrawal>();
        }
    }
}