using Data = Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<Data.Deposit, Deposit>();
        }
    }
}