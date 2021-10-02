namespace Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;

using Data = Playground.PaymentEngine.Store.Allocations.Model;

public class AllocationProfile: Profile {
    public AllocationProfile() {
        CreateMap<Data.Allocation, AutoAllocateResult>();
    }
}