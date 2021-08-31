using GetLastRunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using RunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using GetApprovalRuleHistories = Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using ViewModel = Playground.PaymentEngine.Models.ApprovalRules;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<RunApprovalRules.ApprovalRuleOutcome, ViewModel.ApprovalRuleOutcome>();
            CreateMap<GetApprovalRuleHistories.ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
            CreateMap<GetApprovalRuleHistories.ApprovalRule, ViewModel.ApprovalRule>();
            CreateMap<GetLastRunApprovalRules.ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
            CreateMap<GetLastRunApprovalRules.ApprovalRule, ViewModel.ApprovalRule>();
        }
    }
}