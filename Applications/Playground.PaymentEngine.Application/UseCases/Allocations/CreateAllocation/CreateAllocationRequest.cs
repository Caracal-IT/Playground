namespace Playground.PaymentEngine.Application.UseCases.Allocations.CreateAllocation {
    public record CreateAllocationRequest(long WithdrawalGroupId, long AccountId, decimal Amount, decimal Charge);
}