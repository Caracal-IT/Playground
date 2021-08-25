using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using Playground.Rules;
using Playground.PaymentEngine.Helpers;
using Playground.Rules.CustomActions.Terminal;
using Playground.PaymentEngine.UseCases.Payments.RunApprovalRules;



namespace Playground.PaymentEngine.Setup {
    public static class ApprovalRuleSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<RuleStore, FileRuleStore>();
            builder.Services.AddSingleton<Engine>();

            builder.Services.AddSingleton<RunApprovalRulesUseCase>();
            builder.Services.AddSingleton<TerminalAction>();
        }
    }
}