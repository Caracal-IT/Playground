namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using Playground.PaymentEngine.Rules.Store;
using Playground.PaymentEngine.Rules.Store.File;
using Playground.Rules;
using Playground.Rules.CustomActions.Terminal;

public static class ApprovalRuleSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddTransient<RuleStore, FileRuleStore>();
        builder.Services.AddTransient<TerminalAction>();
        builder.Services.AddTransient<Engine>();

        builder.Services.AddTransient<RunApprovalRulesUseCase>();
        builder.Services.AddTransient<GetApprovalRuleHistoriesUseCase>();
        builder.Services.AddTransient<GetLastRunApprovalRulesUseCase>();
    }
}