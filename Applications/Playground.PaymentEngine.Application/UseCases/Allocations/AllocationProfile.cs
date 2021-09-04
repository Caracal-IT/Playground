using Data = Playground.PaymentEngine.Store.Allocations.Model;

namespace Playground.PaymentEngine.Application.UseCases.Allocations {
    public class AllocationProfile: Profile {
        public AllocationProfile() {
            CreateMap<Data.Allocation, Allocation>().ReverseMap();
        }
    }
}