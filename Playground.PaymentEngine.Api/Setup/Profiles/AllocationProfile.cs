using Playground.PaymentEngine.Api.Models.Allocations;
using AutoAllocate = Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;
using Allocations = Playground.PaymentEngine.Application.UseCases.Allocations;

using ViewModel = Playground.PaymentEngine.Api.Models.Allocations;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class AllocationProfile: Profile {
        public AllocationProfile() {
            CreateMap<AutoAllocate.AutoAllocateResult, AutoAllocateResult>();
            CreateMap<Allocations.Allocation, Allocation>().ReverseMap();
        }
    }
}