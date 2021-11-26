using Playground.PaymentEngine.Store.EF.Accounts;

namespace Playground.PaymentEngine.Api.Setup;

using Microsoft.Extensions.DependencyInjection;
using Store.Accounts;
using Store.Allocations;
using Store.ApprovalRules;
using Store.Customers;
using Store.Deposits;
using Playground.PaymentEngine.Store.File.Accounts;
using Playground.PaymentEngine.Store.File.Allocations;
using Playground.PaymentEngine.Store.File.ApprovalRules;
using Playground.PaymentEngine.Store.File.Customers;
using Playground.PaymentEngine.Store.File.Deposits;
using Playground.PaymentEngine.Store.File.Terminals;
using Playground.PaymentEngine.Store.File.Withdrawals;
using Playground.PaymentEngine.Store.Terminals;
using Store.Withdrawals;

public static class DataSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<WithdrawalStore, FileWithdrawalStore>();
        builder.Services.AddTransient<AccountStore, EFAccountStore>(); //FileAccountStore
        builder.Services.AddSingleton<AllocationStore, FileAllocationStore>();
        builder.Services.AddSingleton<ApprovalRuleStore, FileApprovalRuleStore>();
        builder.Services.AddSingleton<CustomerStore, FileCustomerStore>();
        builder.Services.AddSingleton<TerminalStore, FileTerminalStore>();
        builder.Services.AddSingleton<DepositStore, FileDepositStore>();
    }
}