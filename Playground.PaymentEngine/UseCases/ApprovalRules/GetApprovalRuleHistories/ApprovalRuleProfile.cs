using Data = Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<Data.ApprovalRuleHistory, ApprovalRuleHistory>();
        }
    }
}