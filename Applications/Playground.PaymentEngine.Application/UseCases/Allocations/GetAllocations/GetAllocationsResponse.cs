namespace Playground.PaymentEngine.Application.UseCases.Allocations.GetAllocations;

public record GetAllocationsResponse {
    public IEnumerable<Allocation> Allocations { get; set; } = Array.Empty<Allocation>();
}