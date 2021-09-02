namespace Playground.PaymentEngine.Api.Models.ApprovalRules {
    public class RunApprovalRuleRequest {
        public List<long> WithdrawalGroups { get; set; } = new();
    }
}