using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;

namespace Playground.PaymentEngine.Setup {
    public static class ProcessingSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<ProcessUseCase>();
            builder.Services.AddSingleton<CallbackUseCase>();
        }
    }
}