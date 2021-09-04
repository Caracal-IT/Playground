using Data = Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation {
    public class AllocationProfile: Profile {
        public AllocationProfile() {
            CreateMap<CreateAllocationRequest, Data.Allocation>();
        }
    }
}