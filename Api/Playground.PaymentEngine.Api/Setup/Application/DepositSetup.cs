namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.DeleteDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;

public static class DepositSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddTransient<GetDepositsUseCase>();
        builder.Services.AddTransient<GetDepositUseCase>();
        builder.Services.AddTransient<CreateDepositUseCase>();
        builder.Services.AddTransient<DeleteDepositUseCase>();
    }
}