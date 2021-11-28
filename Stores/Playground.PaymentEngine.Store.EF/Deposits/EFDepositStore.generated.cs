using System;

namespace Playground.PaymentEngine.Store.EF.Deposits; 

public partial class EFDepositStore {
    public EFDepositStore() { }

    public EFDepositStore(DbContextOptions<EFDepositStore> options) : base(options) { }

    protected EFDepositStore(DbContextOptions contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<Deposit>()
            .ToTable("Deposit", "deposits")
            .Property(e => e.DepositDate)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder
            .Entity<MetaData>()
            .ToTable("MetaData", "deposits");
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Deposit>().HasData(DefaultDeposits);
    }

    private static object[] DefaultDeposits => new object[] {
        new {Id = 2L, AccountId = 267L, Amount = 220.0M, TenantId = 1L, DepositDate = new DateTime(2021, 02, 10, 0, 0, 0, 0, DateTimeKind.Utc)},
        new {Id = 3L, AccountId = 267L, Amount = 110.0M, TenantId = 1L, DepositDate = new DateTime(2021, 02, 11, 0, 0, 0, 0, DateTimeKind.Utc)},
        new {Id = 4L, AccountId = 300L, Amount = 200.0M, TenantId = 1L, DepositDate = new DateTime(2021, 06, 15, 0, 0, 0, 0, DateTimeKind.Utc)},
        new {Id = 5L, AccountId = 567L, Amount = 30.0M,  TenantId = 1L, DepositDate = new DateTime(2021, 06, 17, 0, 0, 0, 0, DateTimeKind.Utc)},
        new {Id = 6L, AccountId = 747L, Amount = 670.0M, TenantId = 1L, DepositDate = new DateTime(2021, 07, 15, 0, 0, 0, 0, DateTimeKind.Utc)}
    };
}