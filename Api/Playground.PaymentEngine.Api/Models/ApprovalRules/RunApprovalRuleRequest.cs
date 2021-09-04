namespace Playground.PaymentEngine.Api.Models.ApprovalRules {
    public record RunApprovalRuleRequest {
        public List<long> WithdrawalGroups { get; set; } = new();
    }
}