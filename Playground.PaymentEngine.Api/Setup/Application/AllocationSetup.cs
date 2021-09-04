using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.DeleteAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations;

namespace Playground.PaymentEngine.Api.Setup.Application {
    public static class AllocationSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<AutoAllocateUseCase>();
            builder.Services.AddSingleton<GetAllocationsUseCase>();
            builder.Services.AddSingleton<GetAllocationUseCase>();
            builder.Services.AddSingleton<CreateAllocationUseCase>();
            builder.Services.AddSingleton<DeleteAllocationUseCase>();
        }
    }
}