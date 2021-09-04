using Playground.PaymentEngine.Api.Setup.Application;

namespace Playground.PaymentEngine.Api.Setup {
    public static class ApiSetup {
        public static void Setup(WebApplicationBuilder builder) {
            AllocationSetup.Setup(builder);
            ApprovalRuleSetup.Setup(builder);
            CustomerSetup.Setup(builder);
            DepositSetup.Setup(builder);
            PaymentSetup.Setup(builder);
            WithdrawalSetup.Setup(builder);
            WithdrawalGroupSetup.Setup(builder);
        }
        
        public static void Register(WebApplication _) { }
    }
}