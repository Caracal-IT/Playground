namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.DeleteWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;

public static class WithdrawalSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<CreateWithdrawalUseCase>();
        builder.Services.AddSingleton<GetWithdrawalsUseCase>();
        builder.Services.AddSingleton<GetWithdrawalUseCase>();
        builder.Services.AddSingleton<DeleteWithdrawalUseCase>();
        builder.Services.AddSingleton<ChangeWithdrawalStatusUseCase>();
    }
}