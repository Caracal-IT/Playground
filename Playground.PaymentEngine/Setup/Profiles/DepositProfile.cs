using GetDeposits = Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;
using GetDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;
using CreateDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;

using ViewModel = Playground.PaymentEngine.Models.Deposits;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<ViewModel.CreateDepositRequest, CreateDeposit.CreateDepositRequest>();
            CreateMap<CreateDeposit.Deposit, ViewModel.Deposit>();
            CreateMap<GetDeposits.Deposit, ViewModel.Deposit>();
            CreateMap<GetDeposit.Deposit, ViewModel.Deposit>();
        }
    }
}