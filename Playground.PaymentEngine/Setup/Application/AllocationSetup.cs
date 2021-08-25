using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.Payments.AutoAllocate;

namespace Playground.PaymentEngine.Setup.Application {
    public static class AllocationSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<AutoAllocateUseCase>();
        }
    }
}