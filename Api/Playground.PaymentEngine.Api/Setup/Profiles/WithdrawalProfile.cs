using Playground.PaymentEngine.Api.Models.Withdrawals;
using CreateWithdrawal = Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using Withdrawals = Playground.PaymentEngine.Application.UseCases.Withdrawals;

using ViewModel = Playground.PaymentEngine.Api.Models.Withdrawals;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class WithdrawalProfile: Profile {
        public WithdrawalProfile() {
            CreateMap<CreateWithdrawalRequest, CreateWithdrawal.CreateWithdrawalRequest>();
            CreateMap<Withdrawals.Withdrawal, Withdrawal>();
        }
    }
}