namespace Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocation {
    public record GetAllocationResponse {
        public Allocation? Allocation { get; set; }
    }
}