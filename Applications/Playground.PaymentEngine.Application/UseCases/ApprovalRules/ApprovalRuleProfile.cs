namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules;

using Data = Playground.PaymentEngine.Store.ApprovalRules.Model;

public class ApprovalRuleProfile: Profile {
    public ApprovalRuleProfile() {
        CreateMap<Data.ApprovalRuleHistory, ApprovalRuleHistory>();
        CreateMap<Data.ApprovalRule, ApprovalRule>();
    }
}