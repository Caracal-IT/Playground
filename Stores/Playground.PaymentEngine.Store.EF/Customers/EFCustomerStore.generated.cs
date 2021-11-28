namespace Playground.PaymentEngine.Store.EF.Customers; 

public partial class EFCustomerStore {
    public EFCustomerStore() { }

    public EFCustomerStore(DbContextOptions<EFCustomerStore> options) : base(options) { }

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
        new Customer {Id = 44, Balance = 3400.0M, TenantId = 1},
        new Customer {Id = 74, Balance = 1.0M,    TenantId = 1},
    };
    
    private static object[] DefaultMetadata => new object[] {
        new CustomerMetaData {Id = 1, Name = "firstName", Value = "Ettiene", CustomerId = 44, TenantId = 1},
        new CustomerMetaData {Id = 2, Name = "lastName",  Value = "Mare",    CustomerId = 44, TenantId = 1},
        new CustomerMetaData {Id = 3, Name = "hasKYC",    Value = "true",    CustomerId = 44, TenantId = 1},

        new CustomerMetaData {Id = 4, Name = "firstName", Value = "Kate",    CustomerId = 74, TenantId = 1},
        new CustomerMetaData {Id = 5, Name = "lastName",  Value = "Moss",    CustomerId = 74, TenantId = 1},
    };
}