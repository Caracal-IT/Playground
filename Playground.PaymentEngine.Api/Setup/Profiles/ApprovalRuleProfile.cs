using Playground.PaymentEngine.Api.Models.ApprovalRules;
using ApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules;
using RunApprovalRules = Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;

using ViewModel = Playground.PaymentEngine.Api.Models.ApprovalRules;

namespace Playground.PaymentEngine.Api.Setup.Profiles {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<RunApprovalRules.ApprovalRuleOutcome, ApprovalRuleOutcome>();
            CreateMap<ApprovalRules.ApprovalRuleHistory, ApprovalRuleHistory>();
            CreateMap<ApprovalRules.ApprovalRule, ApprovalRule>();
        }
    }
}