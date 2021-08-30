using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;

namespace Playground.PaymentEngine.Setup.Application {
    public static class PaymentSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<ProcessUseCase>();
            builder.Services.AddSingleton<CallbackUseCase>();
        }
    }
}