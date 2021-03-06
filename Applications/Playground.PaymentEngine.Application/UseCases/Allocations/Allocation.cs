namespace Playground.PaymentEngine.Application.UseCases.Allocations;
    
using Playground.Core.Model;

public record Allocation: Entity {
    public long WithdrawalGroupId { get; set; }
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
    public decimal Charge { get; set; }
    public long AllocationStatusId { get; set; }
    public string? Terminal { get; set; }
    public string? Reference { get; set; }
}