using Data = Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Application.UseCases.Deposits {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<Data.Deposit, Deposit>();
        }
    }
}