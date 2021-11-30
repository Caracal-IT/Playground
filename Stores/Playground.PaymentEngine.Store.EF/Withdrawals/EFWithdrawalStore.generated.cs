namespace Playground.PaymentEngine.Store.EF.Withdrawals; 

public partial class EFWithdrawalStore {
    public EFWithdrawalStore() { }

    public EFWithdrawalStore(DbContextOptions<EFWithdrawalStore> options) : base(options) { }

    protected EFWithdrawalStore(DbContextOptions contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<Withdrawal>()
            .ToTable("Withdrawal", "withdrawals")
            .Property(e => e.DateRequested)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        modelBuilder
            .Entity<WithdrawalStatus>()
            .ToTable("WithdrawalStatus", "withdrawals");
        
        modelBuilder
            .Entity<WithdrawalGroup>()
            .ToTable("WithdrawalGroup", "withdrawals");
        
        modelBuilder
            .Entity<WithdrawalGroup>()
            .Ignore(e => e.WithdrawalIds)
            .Property(e => e.WithdrawalIdsString)
            .HasColumnName("WithdrawalIds");
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<WithdrawalStatus>().HasData(DefaultWithdrawalStatusus);
        modelBuilder.Entity<Withdrawal>().HasData(DefaultWithdrawals);
        modelBuilder.Entity<WithdrawalGroup>().HasData(DefaultWithdrawalGroups);
    }

    private static object[] DefaultWithdrawalStatusus => new object[] {
        new {Id = 1L, Name = "requested",           TenantId = 1L},
        new {Id = 2L, Name = "flashed",             TenantId = 1L},
        new {Id = 3L, Name = "batched",             TenantId = 1L},
        new {Id = 4L, Name = "approved",            TenantId = 1L},
        new {Id = 5L, Name = "rejected",            TenantId = 1L},
        new {Id = 6L, Name = "failed",              TenantId = 1L},
        new {Id = 7L, Name = "partially-processed", TenantId = 1L},
        new {Id = 8L, Name = "processed",           TenantId = 1L},
    };

    private static object[] DefaultWithdrawals => new object[] {
        new { Id = 1L, CustomerId = 44L, Amount = 50M,  WithdrawalStatusId = 3L, DateRequested = new DateTime(2021, 08, 12, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false, TenantId = 1L},
        new { Id = 2L, CustomerId = 44L, Amount = 50M,  WithdrawalStatusId = 3L, DateRequested = new DateTime(2021, 08, 13, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false, TenantId = 1L},
        new { Id = 3L, CustomerId = 74L, Amount = 460M, WithdrawalStatusId = 3L, DateRequested = new DateTime(2021, 08, 17, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false, TenantId = 1L}
    };

    private static object[] DefaultWithdrawalGroups => new object[] {
        new {Id = 1L, CustomerId = 44L, WithdrawalIdsString = "1, 2", TenantId = 1L},
        new {Id = 2L, CustomerId = 74L, WithdrawalIdsString = "3",    TenantId = 1L}
    };
}