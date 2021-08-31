using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using Playground.Rules;
using Playground.Rules.CustomActions.Terminal;

namespace Playground.PaymentEngine.Setup.Application {
    public static class ApprovalRuleSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<RuleStore, FileRuleStore>();
            builder.Services.AddSingleton<TerminalAction>();
            builder.Services.AddSingleton<Engine>();

            builder.Services.AddSingleton<RunApprovalRulesUseCase>();
            builder.Services.AddSingleton<GetApprovalRuleHistoriesUseCase>();
            builder.Services.AddSingleton<GetLastRunApprovalRulesUseCase>();
        }
    }
}