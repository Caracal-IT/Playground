using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        //builder.Services.AddTransient<WithdrawalStore, FileWithdrawalStore>();
        //builder.Services.AddTransient<AccountStore, FileAccountStore>();
        //builder.Services.AddTransient<AllocationStore, EFAllocationStore>();
        //builder.Services.AddTransient<ApprovalRuleStore, FileApprovalRuleStore>();
        //builder.Services.AddTransient<CustomerStore, FileCustomerStore>();
        //builder.Services.AddTransient<TerminalStore, FileTerminalStore>();
        //builder.Services.AddTransient<DepositStore, FileDepositStore>();
        
        builder.Services.AddDbContext<WithdrawalStore, EFWithdrawalStore>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<AccountStore, EFAccountStore>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<AllocationStore, EFAllocationStore>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<ApprovalRuleStore, EFApprovalRuleStore>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<CustomerStore, EFCustomerStore>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<TerminalStore, EFTerminalStore>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
        
        builder.Services.AddDbContext<DepositStore, EFDepositStore>(options => 
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
                ServiceLifetime.Transient
        );
    }
}