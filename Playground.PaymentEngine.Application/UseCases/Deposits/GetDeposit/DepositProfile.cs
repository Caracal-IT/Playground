using Data = Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<Data.Deposit, Deposit>();
        }
    }
}