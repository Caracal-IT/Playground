namespace Playground.PaymentEngine.Application.UseCases.Allocations {
    public class Allocation {
        public long Id { get; set; }
        public long WithdrawalGroupId { get; set; }
        public long AccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Charge { get; set; }
        public long AllocationStatusId { get; set; }
        public string? Terminal { get; set; }
        public string? Reference { get; set; }
    }
}