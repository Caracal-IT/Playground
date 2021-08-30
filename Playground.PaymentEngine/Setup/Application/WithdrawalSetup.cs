using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Playground.PaymentEngine.UseCases.Withdrawals.CreateWithdrawal;
using Playground.PaymentEngine.UseCases.Withdrawals.DeleteWithdrawal;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawal;
using Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.UnGroupWithdrawals;

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