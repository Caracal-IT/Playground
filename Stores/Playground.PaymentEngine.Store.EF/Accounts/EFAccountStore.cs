using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    public DbSet<Account> Accounts { get; set; }
    public DbSet<MetaData> MetaData { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }

    public EFAccountStore() { }

    public EFAccountStore(DbContextOptions<EFAccountStore> options) : base(options) { }

    protected EFAccountStore(DbContextOptions contextOptions) : base(contextOptions) { }

    public AccountStore Clone()
    {
        return new EFAccountStore();
    }
    
    public async Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken)
    {
        return await Accounts.Include(a => a.MetaData)
                             .Where(a => a.Id == id)
                             .FirstOrDefaultAsync(cancellationToken)
                             ??new Account();
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
        
            /*
        modelBuilder
            .Entity<Account>()
            .HasData(
                new Account
                {
                    Id = 1,
                    Exposure = 30.0M,
                    CustomerId = 2,
                    AccountTypeId = 1,
                    IsPreferredAccount = true
                }
            );*/

            modelBuilder
                .Entity<AccountType>()
                .ToTable("AccountType", "accounts")
                .HasData(new AccountType
                {
                    Id = 1,
                    Name = "Visa",
                    ProcessOrder = 0,
                    TenantId = 1
                });
                
        modelBuilder
            .Entity<Account>()
            .ToTable("Account", "accounts")
            .HasOne<AccountType>();
            
        modelBuilder
            .Entity<Account>()
            .HasData(
                new Account {
                    Id = 1,
                    Exposure = 30.0M,
                    CustomerId = 2,
                    AccountTypeId = 1,
                    IsPreferredAccount = true
                });
            
            
            modelBuilder
                .Entity<MetaData>()
                .ToTable("MetaData", "accounts")
                .HasData(
                    new AccountMetaData {
                        Id = 1,
                        Name = "AccountHolder",
                        Value = "Kate",
                        TenantId = 1,
                        AccountId = 1
                    });
        
        base.OnModelCreating(modelBuilder);
    }

    public class AccountMetaData
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public long TenantId { get; set; }
        public long AccountId { get; set; }

    }
}