using Microsoft.Extensions.DependencyInjection;

using Playground.PaymentEngine.Stores.Accounts;
using Playground.PaymentEngine.Stores.Accounts.File;
using Playground.PaymentEngine.Stores.Allocations;
using Playground.PaymentEngine.Stores.Allocations.File;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.Stores.ApprovalRules.File;
using Playground.PaymentEngine.Stores.Customers;
using Playground.PaymentEngine.Stores.Customers.File;
using Playground.PaymentEngine.Stores.Terminals;
using Playground.PaymentEngine.Stores.Terminals.File;
using Playground.PaymentEngine.Stores.Withdrawals;
using Playground.PaymentEngine.Stores.Withdrawals.File;

namespace Playground.PaymentEngine.Setup {
    public static class DataSetup {
        public static void Setup(WebApplicationBuilder builder) {
            builder.Services.AddSingleton<WithdrawalStore, FileWithdrawalStore>();
            builder.Services.AddSingleton<AccountStore, FileAccountStore>();
            builder.Services.AddSingleton<AllocationStore, FileAllocationStore>();
            builder.Services.AddSingleton<ApprovalRuleStore, FileApprovalRuleStore>();
            builder.Services.AddSingleton<CustomerStore, FileCustomerStore>();
            builder.Services.AddSingleton<TerminalStore, FileTerminalStore>();
        }
    }
}