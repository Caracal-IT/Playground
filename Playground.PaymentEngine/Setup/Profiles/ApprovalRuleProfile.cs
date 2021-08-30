using Playground.PaymentEngine.UseCases.ApprovalRules.GetLastRunApprovalRules;
using RunApprovalRules = Playground.PaymentEngine.UseCases.ApprovalRules.RunApprovalRules;
using GetApprovalRuleHistories = Playground.PaymentEngine.UseCases.ApprovalRules.GetApprovalRuleHistories;
using ViewModel = Playground.PaymentEngine.Models.ApprovalRules;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<RunApprovalRules.ApprovalRuleOutcome, ViewModel.ApprovalRuleOutcome>();
            CreateMap<GetApprovalRuleHistories.ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
            CreateMap<ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
        }
    }
}