using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;

namespace Playground.PaymentEngine.Setup.Application {
    public static class DepositSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<GetDepositsUseCase>();
        }
    }
}