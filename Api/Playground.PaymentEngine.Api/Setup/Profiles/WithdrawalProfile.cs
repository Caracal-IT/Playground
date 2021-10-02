namespace Playground.PaymentEngine.Api.Setup.Profiles;

using CreateWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using Withdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals;

using ViewModel = Models.Withdrawals;

public class WithdrawalProfile: Profile {
    public WithdrawalProfile() {
        CreateMap<ViewModel.CreateWithdrawalRequest, CreateWithdrawal.CreateWithdrawalRequest>();
        CreateMap<Withdrawals.Withdrawal, ViewModel.Withdrawal>();
    }
}