using Microsoft.Extensions.DependencyInjection;
using Playground.PaymentEngine.Store.Accounts;
using Playground.PaymentEngine.Store.Allocations;
using Playground.PaymentEngine.Store.ApprovalRules;
using Playground.PaymentEngine.Store.Customers;
using Playground.PaymentEngine.Store.Deposits;
using Playground.PaymentEngine.Store.File.Accounts;
using Playground.PaymentEngine.Store.File.Allocations;
using Playground.PaymentEngine.Store.File.ApprovalRules;
using Playground.PaymentEngine.Store.File.Customers;
using Playground.PaymentEngine.Store.File.Deposits;
using Playground.PaymentEngine.Store.File.Terminals;
using Playground.PaymentEngine.Store.File.Withdrawals;
using Playground.PaymentEngine.Store.Terminals;
using Playground.PaymentEngine.Store.Withdrawals;


namespace Playground.PaymentEngine.Setup {
    public static class DataSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<WithdrawalStore, FileWithdrawalStore>();
            builder.Services.AddSingleton<AccountStore, FileAccountStore>();
            builder.Services.AddSingleton<AllocationStore, FileAllocationStore>();
            builder.Services.AddSingleton<ApprovalRuleStore, FileApprovalRuleStore>();
            builder.Services.AddSingleton<CustomerStore, FileCustomerStore>();
            builder.Services.AddSingleton<TerminalStore, FileTerminalStore>();
            builder.Services.AddSingleton<DepositStore, FileDepositStore>();
        }
    }
}