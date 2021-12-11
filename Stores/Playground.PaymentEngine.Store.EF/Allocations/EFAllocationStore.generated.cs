namespace Playground.PaymentEngine.Store.EF.Allocations; 

public partial class EFAllocationStore {
    private DbContextOptions _options;
    public EFAllocationStore() { }

    public EFAllocationStore(DbContextOptions<EFAllocationStore> options) : base(options) {
        
    }

    protected EFAllocationStore(DbContextOptions contextOptions) : base(contextOptions) {
        _options = contextOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<AllocationStatus>()
            .ToTable("AccountType", "allocations");
        
        modelBuilder
            .Entity<Allocation>()
            .ToTable("Allocation", "allocations")
            .HasOne<AllocationStatus>();;
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<AllocationStatus>().HasData(DefaultAllocationStatusus);
        modelBuilder.Entity<Allocation>().HasData(DefaultAllocations);
    }
    
    private static object[] DefaultAllocationStatusus => new object[] {
        new {Id = 1L, Name = "none",          TenantId = 1L},
        new {Id = 2L, Name = "allocated",     TenantId = 1L},
        new {Id = 3L, Name = "refunded",      TenantId = 1L},
        new {Id = 4L, Name = "confiscated",   TenantId = 1L},
        new {Id = 5L, Name = "paid",          TenantId = 1L},
        new {Id = 6L, Name = "rejected",      TenantId = 1L},
        new {Id = 7L, Name = "callback-paid", TenantId = 1L}
    };
    
    private static object[] DefaultAllocations => new object[] {
        new {Id = 189L, WithdrawalGroupId = 1L, AccountId = 267L,  Amount = 13M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 190L, WithdrawalGroupId = 1L, AccountId = 567L,  Amount = 27M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 200L, WithdrawalGroupId = 1L, AccountId = 300L,  Amount = 18M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 201L, WithdrawalGroupId = 1L, AccountId = 267L,  Amount = 12M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 202L, WithdrawalGroupId = 1L, AccountId = 300L,  Amount = 21M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 203L, WithdrawalGroupId = 1L, AccountId = 300L,  Amount = 29M,   Charge = 0M, AllocationStatusId = 2L, TenantId = 1L},
        new {Id = 671L, WithdrawalGroupId = 1L, AccountId = 747L,  Amount = 670M,  Charge = 0M, AllocationStatusId = 5L, TenantId = 1L, Terminal = "Rebilly", Reference = "EF45_66_88"}
    };
}