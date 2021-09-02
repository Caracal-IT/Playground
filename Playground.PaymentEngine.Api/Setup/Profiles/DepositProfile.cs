using Playground.PaymentEngine.Api.Models.Deposits;
using GetDeposits = Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;
using GetDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;
using CreateDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;

using ViewModel = Playground.PaymentEngine.Api.Models.Deposits;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<CreateDepositRequest, CreateDeposit.CreateDepositRequest>();
            CreateMap<CreateDeposit.Deposit, Deposit>();
            CreateMap<GetDeposits.Deposit, Deposit>();
            CreateMap<GetDeposit.Deposit, Deposit>();
        }
    }
}