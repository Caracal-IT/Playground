namespace Playground.PaymentEngine.Api.Setup.Profiles;

using Deposits = Playground.PaymentEngine.Application.UseCases.Deposits;
using CreateDeposit = Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;

using ViewModel = Models.Deposits;
    
public class DepositProfile: Profile {
    public DepositProfile() {
        CreateMap<ViewModel.CreateDepositRequest, CreateDeposit.CreateDepositRequest>();
        CreateMap<Deposits.Deposit, ViewModel.Deposit>();
    }
}