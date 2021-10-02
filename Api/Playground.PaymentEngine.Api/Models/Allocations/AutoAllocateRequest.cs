namespace Playground.PaymentEngine.Api.Models.Allocations;

public record AutoAllocateRequest {
    public List<long> WithdrawalGroups { get; set; }
}