using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.UnGroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.AppendGroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.CreateWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.DeleteWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal;
using Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals;

namespace Playground.PaymentEngine.Setup.Application {
    public static class WithdrawalSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<CreateWithdrawalUseCase>();
            builder.Services.AddSingleton<GetWithdrawalsUseCase>();
            builder.Services.AddSingleton<GetWithdrawalUseCase>();
            builder.Services.AddSingleton<DeleteWithdrawalUseCase>();
            builder.Services.AddSingleton<ChangeWithdrawalStatusUseCase>();
            
            builder.Services.AddSingleton<GroupWithdrawalsUseCase>();
            builder.Services.AddSingleton<UnGroupWithdrawalsUseCase>();
            builder.Services.AddSingleton<GetWithdrawalGroupsUseCase>();
            builder.Services.AddSingleton<AppendGroupWithdrawalsUseCase>();
        }
    }
}