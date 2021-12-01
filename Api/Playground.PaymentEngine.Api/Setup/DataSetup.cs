using Playground.PaymentEngine.Store.EF.Accounts;
using Playground.PaymentEngine.Store.EF.Allocations;
using Playground.PaymentEngine.Store.EF.ApprovalRules;
using Playground.PaymentEngine.Store.EF.Customers;
using Playground.PaymentEngine.Store.EF.Deposits;
using Playground.PaymentEngine.Store.EF.Terminals;
using Playground.PaymentEngine.Store.EF.Withdrawals;

using Microsoft.Extensions.DependencyInjection;

using Playground.PaymentEngine.Store.Accounts;
using Playground.PaymentEngine.Store.Allocations;
using Playground.PaymentEngine.Store.ApprovalRules;
using Playground.PaymentEngine.Store.Customers;
using Playground.PaymentEngine.Store.Deposits;
using Playground.PaymentEngine.Store.Terminals;
using Playground.PaymentEngine.Store.Withdrawals;

namespace Playground.PaymentEngine.Api.Setup;

public static class DataSetup {
    public static void Setup(WebApplicationBuilder builder) {
        builder.Services.AddTransient<WithdrawalStore, EFWithdrawalStore>(); // FileWithdrawalStore
        builder.Services.AddTransient<AccountStore, EFAccountStore>(); //FileAccountStore
        builder.Services.AddTransient<AllocationStore, EFAllocationStore>(); // FileAllocationStore
        builder.Services.AddTransient<ApprovalRuleStore, EFApprovalRuleStore>(); // FileApprovalRuleStore
        builder.Services.AddTransient<CustomerStore, EFCustomerStore>(); //FileCustomerStore
        builder.Services.AddTransient<TerminalStore, EFTerminalStore>(); // FileTerminalStore
        builder.Services.AddTransient<DepositStore, EFDepositStore>(); //FileDepositStore
    }
}