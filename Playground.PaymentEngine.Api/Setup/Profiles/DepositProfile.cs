using Playground.PaymentEngine.Api.Models.Deposits;
using Deposits = Playground.PaymentEngine.Application.UseCases.Deposits;
using CreateDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;

using ViewModel = Playground.PaymentEngine.Api.Models.Deposits;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<CreateDepositRequest, CreateDeposit.CreateDepositRequest>();
            CreateMap<Deposits.Deposit, Deposit>();
        }
    }
}