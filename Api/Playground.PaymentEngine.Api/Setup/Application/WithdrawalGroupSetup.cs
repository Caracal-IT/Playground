namespace Playground.PaymentEngine.Api.Setup.Application;

using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.AppendGroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.UnGroupWithdrawals;

public static class WithdrawalGroupSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<GroupWithdrawalsUseCase>();
        builder.Services.AddSingleton<UnGroupWithdrawalsUseCase>();
        builder.Services.AddSingleton<GetWithdrawalGroupsUseCase>();
        builder.Services.AddSingleton<AppendGroupWithdrawalsUseCase>();
    }
}