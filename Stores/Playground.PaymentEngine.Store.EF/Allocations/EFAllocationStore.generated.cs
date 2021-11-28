namespace Playground.PaymentEngine.Store.EF.Allocations; 

public partial class EFAllocationStore {
    public EFAllocationStore() { }

    public EFAllocationStore(DbContextOptions<EFAllocationStore> options) : base(options) { }

    protected EFAllocationStore(DbContextOptions contextOptions) : base(contextOptions) { }

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
        new AllocationStatus {Id = 1, Name = "none",     TenantId = 1},
        new AllocationStatus {Id = 2, Name = "allocated",     TenantId = 1},
        new AllocationStatus {Id = 3, Name = "refunded",      TenantId = 1},
        new AllocationStatus {Id = 4, Name = "confiscated",   TenantId = 1},
        new AllocationStatus {Id = 5, Name = "paid",          TenantId = 1},
        new AllocationStatus {Id = 6, Name = "rejected",      TenantId = 1},
        new AllocationStatus {Id = 7, Name = "callback-paid", TenantId = 1},
    };
    
    private static object[] DefaultAllocations => new object[] {
        new Allocation {Id = 189, WithdrawalGroupId = 1, AccountId = 267,  Amount = 13M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 190, WithdrawalGroupId = 1, AccountId = 567,  Amount = 27M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 200, WithdrawalGroupId = 1, AccountId = 300,  Amount = 18M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 201, WithdrawalGroupId = 1, AccountId = 267,  Amount = 12M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 202, WithdrawalGroupId = 1, AccountId = 300,  Amount = 21M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 203, WithdrawalGroupId = 1, AccountId = 300,  Amount = 29M,   Charge = 0M, AllocationStatusId = 2, TenantId = 1},
        new Allocation {Id = 671, WithdrawalGroupId = 1, AccountId = 747,  Amount = 670M,  Charge = 0M, AllocationStatusId = 5, TenantId = 1, Terminal = "Rebilly", Reference = "EF45_66_88"},
    };
}