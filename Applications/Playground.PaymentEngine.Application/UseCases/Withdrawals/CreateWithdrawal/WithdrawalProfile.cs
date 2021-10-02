namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;

using Data = Playground.PaymentEngine.Store.Withdrawals.Model;

public class WithdrawalProfile : Profile {
    public WithdrawalProfile() {
        CreateMap<CreateWithdrawalRequest, Data.Withdrawal>();
    }
}