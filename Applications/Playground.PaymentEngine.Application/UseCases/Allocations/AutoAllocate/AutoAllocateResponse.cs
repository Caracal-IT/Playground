namespace Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;

public record AutoAllocateResponse {
    public List<AutoAllocateResult> AllocationResults { get; set; } = new();
}