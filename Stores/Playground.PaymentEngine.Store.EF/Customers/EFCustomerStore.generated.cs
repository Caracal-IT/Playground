namespace Playground.PaymentEngine.Store.EF.Customers; 

public partial class EFCustomerStore {
    private DbContextOptions _options;
    
    public EFCustomerStore() { }

    public EFCustomerStore(DbContextOptions<EFCustomerStore> options) : base(options) {
        _options = options;
    }

    protected EFCustomerStore(DbContextOptions contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<Customer>()
            .ToTable("Customer", "customers");

        modelBuilder
            .Entity<MetaData>()
            .ToTable("MetaData", "customers");
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Customer>().HasData(DefaultCustomers);
        modelBuilder.Entity<MetaData>().HasData(DefaultMetadata);
    }
    
    private static object[] DefaultCustomers => new object[] {
        new {Id = 44L, Balance = 3400M, TenantId = 1L},
        new {Id = 74L, Balance = 1M,    TenantId = 1L}
    };
    
    private static object[] DefaultMetadata => new object[] {
        new {Id = 1L, Name = "firstName", Value = "Ettiene", CustomerId = 44L, TenantId = 1L},
        new {Id = 2L, Name = "lastName",  Value = "Mare",    CustomerId = 44L, TenantId = 1L},
        new {Id = 3L, Name = "hasKYC",    Value = "true",    CustomerId = 44L, TenantId = 1L},

        new {Id = 4L, Name = "firstName", Value = "Kate",    CustomerId = 74L, TenantId = 1L},
        new {Id = 5L, Name = "lastName",  Value = "Moss",    CustomerId = 74L, TenantId = 1L}
    };
}