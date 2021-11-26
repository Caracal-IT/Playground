namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.DeleteAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocation;
using Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations;

public static class AllocationSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddTransient<AutoAllocateUseCase>();
        builder.Services.AddTransient<GetAllocationsUseCase>();
        builder.Services.AddTransient<GetAllocationUseCase>();
        builder.Services.AddTransient<CreateAllocationUseCase>();
        builder.Services.AddTransient<DeleteAllocationUseCase>();
    }
}