namespace Playground.PaymentEngine.Models.Allocations {
    public record AutoAllocateRequest {
        public List<long> WithdrawalGroups { get; set; }
    }
}