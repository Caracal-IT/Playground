namespace Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation;

using Data = Playground.PaymentEngine.Store.Allocations.Model;

public class AllocationProfile: Profile {
    public AllocationProfile() {
        CreateMap<CreateAllocationRequest, Data.Allocation>();
    }
}