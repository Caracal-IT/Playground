using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using Playground.PaymentEngine.Rules.Store;
using Playground.PaymentEngine.Rules.Store.File;
using Playground.Rules;
using Playground.Rules.CustomActions.Terminal;

namespace Playground.PaymentEngine.Api.Setup.Application {
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