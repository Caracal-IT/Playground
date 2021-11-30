namespace Playground.PaymentEngine.Store.EF.Terminals; 

public partial class EFTerminalStore {
    public EFTerminalStore() { }

    public EFTerminalStore(DbContextOptions<EFTerminalStore> options) : base(options) { }

    protected EFTerminalStore(DbContextOptions contextOptions) : base(contextOptions) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<Terminal>()
            .ToTable("Terminal", "terminals");
        
        modelBuilder
            .Entity<Setting>()
            .ToTable("Setting", "terminals");
        
        modelBuilder
            .Entity<TerminalMap>()
            .ToTable("TerminalMap", "terminals");
        
        modelBuilder
            .Entity<TerminalResult>()
            .ToTable("TerminalResult", "terminals")
            .Property(e => e.Date)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder
            .Entity<MetaData>()
            .ToTable("MetaData", "terminals");
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Terminal>().HasData(DefaultTerminals);
        modelBuilder.Entity<Setting>().HasData(DefaultSettings);
        modelBuilder.Entity<TerminalMap>().HasData(DefaultMaps);
        modelBuilder.Entity<TerminalResult>().HasData(DefaultTerminalResult);
        modelBuilder.Entity<MetaData>().HasData(DefaultTerminalResultMetaData);
    }
    
    private static object[] DefaultTerminals => new object[] {
        new {Id = 1L, Name = "Rebilly",        RetryCount = 2, Type = "http",   TenantId = 1L},
        new {Id = 2L, Name = "CustomTerminal", RetryCount = 2, Type = "http",   TenantId = 1L},
        new {Id = 3L, Name = "GlobalPay",      RetryCount = 2, Type = "http",   TenantId = 1L},
        new {Id = 4L, Name = "Rebilly_File",   RetryCount = 1, Type = "stream", TenantId = 1L},
        new {Id = 5L, Name = "Orca",           RetryCount = 1, Type = "http",   TenantId = 1L}
    };

    private static object[] DefaultSettings => new object[] {
        new {Id = 1L, TerminalId = 1L, Name = "header:auth-user", Value = "Ettiene Mare", TenantId = 1L},
        new {Id = 2L, TerminalId = 4L, Name = "file_path", Value = "Exports", TenantId = 1L},
    };

    private static object[] DefaultMaps => new object[] {
        new { Id = 1L, TerminalId = 1L, AccountTypeId = 88L, Enabled = true, Order = (short) 0, TenantId = 1L},
        new { Id = 2L, TerminalId = 2L, AccountTypeId = 98L, Enabled = true, Order = (short) 0, TenantId = 1L},
        new { Id = 3L, TerminalId = 3L, AccountTypeId = 88L, Enabled = true, Order = (short) 1, TenantId = 1L},
        new { Id = 4L, TerminalId = 4L, AccountTypeId = 90L, Enabled = true, Order = (short) 1, TenantId = 1L}
    };

    private static object[] DefaultTerminalResult => new object[] {
        new { Id = 1L, Code = "00", Terminal = "Rebilly", Reference = "44543434", Message = "Test Message", Success = false, Date = new DateTime(2000, 01, 17, 0, 0, 0, DateTimeKind.Utc), TenantId = 1L}
    };

    private static object[] DefaultTerminalResultMetaData => new object[] {
        new {Id = 1L, TerminalResultId = 1L, Name = "account-holder", Value = "E.L. Mare", TenantId = 1L},
        new {Id = 2L, TerminalResultId = 1L, Name = "card-number", Value = "123556456", TenantId = 1L},
        new {Id = 3L, TerminalResultId = 1L, Name = "cvc", Value = "547", TenantId = 1L},
    };
}