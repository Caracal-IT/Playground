namespace Playground.PaymentEngine.Api.Models.ApprovalRules {
    public class ApprovalRule {
        public string RuleName { get; set; }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
    }
}