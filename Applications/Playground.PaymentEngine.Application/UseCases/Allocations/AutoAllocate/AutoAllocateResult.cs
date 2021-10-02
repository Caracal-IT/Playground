namespace Playground.PaymentEngine.Application.UseCases.Allocations.AutoAllocate;

public record AutoAllocateResult {
    public long AllocationId { get; set; }
    public long WithdrawalGroupId { get; set; }
    public decimal Amount { get; set; }
    public long AccountId { get; set; }
}