namespace Playground.PaymentEngine.Api.Setup.Profiles;

using AutoAllocate = Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using CreateAllocation = Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation;
using Allocations = Playground.PaymentEngine.Application.UseCases.Allocations;

using ViewModel = Models.Allocations;

public class AllocationProfile: Profile {
    public AllocationProfile() {
        CreateMap<AutoAllocate.AutoAllocateResult, ViewModel.AutoAllocateResult>();
        CreateMap<ViewModel.CreateAllocationRequest, CreateAllocation.CreateAllocationRequest>();
        CreateMap<Allocations.Allocation, ViewModel.Allocation>().ReverseMap();
    }
}