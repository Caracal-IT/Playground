using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Playground.PaymentEngine.UseCases.Withdrawals.DeleteWithdrawal;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;

namespace Playground.PaymentEngine.Setup.Application {
    public static class WithdrawalSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<GetWithdrawalsUseCase>();
            builder.Services.AddSingleton<GetWithdrawalUseCase>();
            builder.Services.AddSingleton<DeleteWithdrawalUseCase>();
            builder.Services.AddSingleton<ChangeWithdrawalStatusUseCase>();
        }
    }
}