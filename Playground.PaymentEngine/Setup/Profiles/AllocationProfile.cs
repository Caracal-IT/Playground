using AutoAllocate = Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;

using ViewModel = Playground.PaymentEngine.Models.Allocations;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class AllocationProfile: Profile {
        public AllocationProfile() {
            CreateMap<AutoAllocate.AutoAllocateResult, ViewModel.AutoAllocateResult>();
        }
    }
}