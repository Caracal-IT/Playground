namespace Playground.PaymentEngine.Application.UseCases.Allocations;

using Data = Playground.PaymentEngine.Store.Allocations.Model;

public class AllocationProfile: Profile {
    public AllocationProfile() {
        CreateMap<Data.Allocation, Allocation>().ReverseMap();
    }
}