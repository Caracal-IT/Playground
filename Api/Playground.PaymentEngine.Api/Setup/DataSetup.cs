using Playground.PaymentEngine.Store.EF.Accounts;
using Playground.PaymentEngine.Store.EF.Allocations;
using Playground.PaymentEngine.Store.EF.ApprovalRules;
using Playground.PaymentEngine.Store.EF.Customers;
using Playground.PaymentEngine.Store.EF.Deposits;
using Playground.PaymentEngine.Store.EF.Terminals;

using Microsoft.Extensions.DependencyInjection;

using Playground.PaymentEngine.Store.Accounts;
using Playground.PaymentEngine.Store.Allocations;
using Playground.PaymentEngine.Store.ApprovalRules;
using Playground.PaymentEngine.Store.Customers;
using Playground.PaymentEngine.Store.Deposits;
using Playground.PaymentEngine.Store.File.Allocations;
using Playground.PaymentEngine.Store.File.Terminals;
using Playground.PaymentEngine.Store.File.Withdrawals;
using Playground.PaymentEngine.Store.Terminals;
using Playground.PaymentEngine.Store.Withdrawals;

namespace Playground.PaymentEngine.Api.Setup;

public static class DataSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddSingleton<WithdrawalStore, FileWithdrawalStore>(); // FileWithdrawalStore
        builder.Services.AddTransient<AccountStore, EFAccountStore>(); //FileAccountStore
        builder.Services.AddSingleton<AllocationStore, FileAllocationStore>(); // FileAllocationStore
        builder.Services.AddSingleton<ApprovalRuleStore, EFApprovalRuleStore>(); // FileApprovalRuleStore
        builder.Services.AddSingleton<CustomerStore, EFCustomerStore>(); //FileCustomerStore
        builder.Services.AddSingleton<TerminalStore, FileTerminalStore>(); // FileTerminalStore
        builder.Services.AddSingleton<DepositStore, EFDepositStore>(); //FileDepositStore
    }
}