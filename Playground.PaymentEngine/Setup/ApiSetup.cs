using Playground.PaymentEngine.Setup.Application;

namespace Playground.PaymentEngine.Setup {
    public static class ApiSetup {
        public static void Setup(WebApplicationBuilder builder) {
            AllocationSetup.Setup(builder);
            ApprovalRuleSetup.Setup(builder);
            ProcessingSetup.Setup(builder);
            WithdrawalSetup.Setup(builder);
        }
        
        public static void Register(WebApplication app) {
          //  WithdrawalSetup.Register(app);
        }
    }
}