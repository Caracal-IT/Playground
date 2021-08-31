using Data = Playground.PaymentEngine.Store.ApprovalRules.Model;

namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<Data.ApprovalRuleHistory, ApprovalRuleHistory>();
            CreateMap<Data.ApprovalRule, ApprovalRule>();
        }
    }
}