// ReSharper disable InconsistentNaming

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Playground.PaymentEngine.Store.Accounts;
using Playground.PaymentEngine.Store.Accounts.Model;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.EF.Accounts;

public class EFAccountStore: DbContext, AccountStore
{
    private DbSet<Account> Accounts { get; set; } = null!;
    private DbSet<AccountType> AccountTypes { get; set; } = null!;

    public EFAccountStore() { }

    public EFAccountStore(DbContextOptions<EFAccountStore> options) : base(options) { }

    protected EFAccountStore(DbContextOptions contextOptions) : base(contextOptions) { }
    
    public IQueryable<Account> GetAccounts() =>
        Accounts.Include(a => a.MetaData);
    
    public async Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken)
    {
        return await Accounts.Include(a => a.MetaData)
                             .Where(a => a.Id == id)
                             .FirstOrDefaultAsync(cancellationToken)
                             ??new Account{Id = id};
    }

    public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken)
    {
        return await Accounts
            .Include(a => a.MetaData)
            .Where(a => a.CustomerId == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken)
    {
        return await AccountTypes.Where(a => accountTypeIds.Contains(a.Id))
                                 .ToListAsync(cancellationToken);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<AccountType>()
            .ToTable("AccountType", "accounts");
        
        modelBuilder
            .Entity<Account>()
            .ToTable("Account", "accounts")
            .HasOne<AccountType>();

        modelBuilder
            .Entity<MetaData>()
            .ToTable("MetaData", "accounts");

        modelBuilder.Entity<AccountType>().HasData(GetDefaultAccountTypes());
        modelBuilder.Entity<Account>().HasData(GetDefaultAccounts());
        modelBuilder.Entity<MetaData>().HasData(GetDefaultMetadata());
        
        base.OnModelCreating(modelBuilder);
    }

    private object[] GetDefaultAccountTypes() => new object[] {
        new AccountType {Id = 88, Name = "Visa",       ProcessOrder = 0, TenantId = 1},
        new AccountType {Id = 90, Name = "MasterCard", ProcessOrder = 1, TenantId = 1},
        new AccountType {Id = 98, Name = "PayPal",     ProcessOrder = 2, TenantId = 1}
    };
    
    private object[] GetDefaultAccounts() => new object[] {
        new Account {Id = 267, AccountTypeId = 88, CustomerId = 2,  Exposure = 30.0M,  IsPreferredAccount = false, TenantId = 1},
        new Account {Id = 300, AccountTypeId = 90, CustomerId = 44, Exposure = 132.0M, IsPreferredAccount = true,  TenantId = 1},
        new Account {Id = 567, AccountTypeId = 98, CustomerId = 44, Exposure = 3.0M,   IsPreferredAccount = false, TenantId = 1},
        new Account {Id = 747, AccountTypeId = 88, CustomerId = 74, Exposure = 0M,     IsPreferredAccount = false, TenantId = 1}
    };
    
    private object[] GetDefaultMetadata() => new object[] {
        new AccountMetaData { Id = 1,  Name = "account-holder", Value = "E.L. Marè",        AccountId = 267, TenantId = 1},
        new AccountMetaData { Id = 2,  Name = "card-number",    Value = "123556456",        AccountId = 267, TenantId = 1},
        new AccountMetaData { Id = 3,  Name = "cvc",            Value = "547",              AccountId = 267, TenantId = 1},
                
        new AccountMetaData { Id = 4,  Name = "account-holder", Value = "Kate Summers",     AccountId = 300, TenantId = 1},
        new AccountMetaData { Id = 5,  Name = "card-number",    Value = "556688112",        AccountId = 300, TenantId = 1},
        new AccountMetaData { Id = 6,  Name = "cvc",            Value = "556",              AccountId = 300, TenantId = 1},
                
        new AccountMetaData { Id = 7,  Name = "account",        Value = "ettiene@test.com", AccountId = 567, TenantId = 1},
        new AccountMetaData { Id = 8,  Name = "sub-account",    Value = "savings",          AccountId = 567, TenantId = 1},
                
        new AccountMetaData { Id = 9,  Name = "account-holder", Value = "Kate Moss",        AccountId = 747, TenantId = 1},
        new AccountMetaData { Id = 10, Name = "card-number",    Value = "8675894885776",    AccountId = 747, TenantId = 1},
        new AccountMetaData { Id = 11, Name = "cvc",            Value = "101",              AccountId = 747, TenantId = 1}
    };

    // ReSharper disable UnusedAutoPropertyAccessor.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public class AccountMetaData
    {
        public AccountMetaData() => Name = null!;

        public long Id { get; set; }
        public string Name { get; set; }
        public string? Value { get; set; }
        public long TenantId { get; set; }
        public long AccountId { get; set; }
    }
}