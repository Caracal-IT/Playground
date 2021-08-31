namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class ApprovalRule {
        public string RuleName { get; set; } = "Rule1";
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
    }
}