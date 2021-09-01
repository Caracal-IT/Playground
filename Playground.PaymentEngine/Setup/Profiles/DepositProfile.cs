using GetDeposits = Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;

using ViewModel = Playground.PaymentEngine.Models.Deposits;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class DepositProfile: Profile {
        public DepositProfile() {
            CreateMap<GetDeposits.Deposit, ViewModel.Deposit>();
        }
    }
}