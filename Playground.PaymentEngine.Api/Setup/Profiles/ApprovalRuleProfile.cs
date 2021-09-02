using Playground.PaymentEngine.Api.Models.ApprovalRules;
using GetLastRunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using RunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using GetApprovalRuleHistories = Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using ViewModel = Playground.PaymentEngine.Api.Models.ApprovalRules;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<RunApprovalRules.ApprovalRuleOutcome, ApprovalRuleOutcome>();
            CreateMap<GetApprovalRuleHistories.ApprovalRuleHistory, ApprovalRuleHistory>();
            CreateMap<GetApprovalRuleHistories.ApprovalRule, ApprovalRule>();
            CreateMap<GetLastRunApprovalRules.ApprovalRuleHistory, ApprovalRuleHistory>();
            CreateMap<GetLastRunApprovalRules.ApprovalRule, ApprovalRule>();
        }
    }
}