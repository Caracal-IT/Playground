namespace Playground.PaymentEngine.Api.Setup.Profiles;

using ApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules;
using RunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;

using ViewModel = Models.ApprovalRules;

public class ApprovalRuleProfile: Profile {
    public ApprovalRuleProfile() {
        CreateMap<RunApprovalRules.ApprovalRuleOutcome, ViewModel.ApprovalRuleOutcome>();
        CreateMap<ApprovalRules.ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
        CreateMap<ApprovalRules.ApprovalRule, ViewModel.ApprovalRule>();
    }
}