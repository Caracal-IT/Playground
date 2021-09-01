using Data = Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<CreateDepositRequest, Data.Deposit>();
            CreateMap<Data.Deposit, Deposit>();
        }
    }
}