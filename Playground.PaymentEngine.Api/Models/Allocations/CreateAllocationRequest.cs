namespace Playground.PaymentEngine.Api.Models.Allocations {
    public record CreateAllocationRequest(long WithdrawalGroupId, long AccountId, decimal Amount, decimal Charge);
}