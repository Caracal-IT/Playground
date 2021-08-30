using Data = Playground.PaymentEngine.Stores.ApprovalRules.Model;


using RunApprovalRules = Playground.PaymentEngine.UseCases.ApprovalRules.RunApprovalRules;

using ViewModel = Playground.PaymentEngine.Models.ApprovalRules;

namespace Playground.PaymentEngine.Setup.Profiles {
    public class ApprovalRuleProfile: Profile {
        public ApprovalRuleProfile() {
            CreateMap<RunApprovalRules.ApprovalRuleOutcome, ViewModel.ApprovalRuleOutcome>();
            CreateMap<Data.ApprovalRuleHistory, ViewModel.ApprovalRuleHistory>();
        }
    }
}