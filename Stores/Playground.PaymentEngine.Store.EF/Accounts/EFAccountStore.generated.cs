// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Accounts;

public partial class EFAccountStore {
    private DbContextOptions _options;
    
    public EFAccountStore() { }

    public EFAccountStore(DbContextOptions<EFAccountStore> options) : base(options) {
        _options = options;
    }

    protected EFAccountStore(DbContextOptions contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
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
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<AccountType>().HasData(DefaultAccountTypes);
        modelBuilder.Entity<Account>().HasData(DefaultAccounts);
        modelBuilder.Entity<MetaData>().HasData(DefaultMetadata);
    }

    private static object[] DefaultAccountTypes => new object[] {
        new {Id = 88L, Name = "Visa",       Charge = 0M, Threshold = 500M, ProcessOrder = 0, TenantId = 1L},
        new {Id = 90L, Name = "MasterCard", Charge = 0M, Threshold = 500M, ProcessOrder = 1, TenantId = 1L},
        new {Id = 98L, Name = "PayPal",     Charge = 0M, Threshold = 0M,   ProcessOrder = 2, TenantId = 1L}
    };

    private static object[] DefaultAccounts => new object[] {
        new {Id = 267L, AccountTypeId = 88L, CustomerId = 2L,  Exposure = 30.0M,  IsPreferredAccount = false, TenantId = 1L},
        new {Id = 300L, AccountTypeId = 90L, CustomerId = 44L, Exposure = 132.0M, IsPreferredAccount = true,  TenantId = 1L},
        new {Id = 567L, AccountTypeId = 98L, CustomerId = 44L, Exposure = 3.0M,   IsPreferredAccount = false, TenantId = 1L},
        new {Id = 747L, AccountTypeId = 88L, CustomerId = 74L, Exposure = 0M,     IsPreferredAccount = false, TenantId = 1L}
    };

    private static object[] DefaultMetadata => new object[] {
        new {Id = 1L,  Name = "account-holder", Value = "E.L. Marè",        AccountId = 267L, TenantId = 1L},
        new {Id = 2L,  Name = "card-number",    Value = "123556456",        AccountId = 267L, TenantId = 1L},
        new {Id = 3L,  Name = "cvc",            Value = "547",              AccountId = 267L, TenantId = 1L},

        new {Id = 4L,  Name = "account-holder", Value = "Kate Summers",     AccountId = 300L, TenantId = 1L},
        new {Id = 5L,  Name = "card-number",    Value = "556688112",        AccountId = 300L, TenantId = 1L},
        new {Id = 6L,  Name = "cvc",            Value = "556",              AccountId = 300L, TenantId = 1L},

        new {Id = 7L,  Name = "account",        Value = "ettiene@test.com", AccountId = 567L, TenantId = 1L},
        new {Id = 8L,  Name = "sub-account",    Value = "savings",          AccountId = 567L, TenantId = 1L},

        new {Id = 9L,  Name = "account-holder", Value = "Kate Moss",        AccountId = 747L, TenantId = 1L},
        new {Id = 10L, Name = "card-number",    Value = "8675894885776",    AccountId = 747L, TenantId = 1L},
        new {Id = 11L, Name = "cvc",            Value = "101",              AccountId = 747L, TenantId = 1L}
    };
}