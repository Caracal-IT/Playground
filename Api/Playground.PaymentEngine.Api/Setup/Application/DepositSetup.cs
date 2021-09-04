using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.DeleteDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;

namespace Playground.PaymentEngine.Api.Setup.Application {
    public static class DepositSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<GetDepositsUseCase>();
            builder.Services.AddSingleton<GetDepositUseCase>();
            builder.Services.AddSingleton<CreateDepositUseCase>();
            builder.Services.AddSingleton<DeleteDepositUseCase>();
        }
    }
}