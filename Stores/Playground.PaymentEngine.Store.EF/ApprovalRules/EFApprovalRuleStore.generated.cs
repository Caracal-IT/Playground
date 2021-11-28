using System;

namespace Playground.PaymentEngine.Store.EF.ApprovalRules; 

public partial class EFApprovalRuleStore {
    public EFApprovalRuleStore() { }

    public EFApprovalRuleStore(DbContextOptions<EFApprovalRuleStore> options) : base(options) { }

    protected EFApprovalRuleStore(DbContextOptions contextOptions) : base(contextOptions) { }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5433;Database=playground;User Id=postgres;Password=postgress;");
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        CreateModel(modelBuilder);
        CreateDefaults(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void CreateModel(ModelBuilder modelBuilder) {
        modelBuilder
            .Entity<ApprovalRuleHistory>()
            .ToTable("ApprovalRuleHistory", "approval_rules")
            .Property(e => e.TransactionDate)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        modelBuilder
            .Entity<ApprovalRule>()
            .ToTable("ApprovalRule", "approval_rules");

        modelBuilder
            .Entity<MetaData>()
            .ToTable("MetaData", "approval_rules");
    }

    private static void CreateDefaults(ModelBuilder modelBuilder) {
        modelBuilder.Entity<ApprovalRuleHistory>().HasData(DefaultApprovalRuleHistory);
        modelBuilder.Entity<ApprovalRule>().HasData(DefaultApprovalRule);
        modelBuilder.Entity<MetaData>().HasData(DefaultMetadata);
    }

    private static object[] DefaultApprovalRuleHistory => new object[] {
        new {Id = 1L, WithdrawalGroupId = 2L, TransactionId = new Guid("8c5c1ae6-b2ab-4715-aebe-258a64aee52d"), TransactionDate = new DateTime(2020, 02, 11, 0, 0, 0, DateTimeKind.Utc), TenantId = 1L}
    };
    
    private static object[] DefaultApprovalRule => new object[] {
        new {Id = 1L, RuleName = "rule1", IsSuccessful = true, Message = "Rule 1 succeeded", ApprovalRuleHistoryId = 1L, TenantId = 1L},
        new {Id = 2L, RuleName = "rule2", IsSuccessful = true, Message = "Rule 2 succeeded", ApprovalRuleHistoryId = 1L, TenantId = 1L}
    };
    
    private static object[] DefaultMetadata => new object[] {
        new {Id = 1L, Name = "withdrawal-id", Value = "1", ApprovalRuleHistoryId = 1L, TenantId = 1L}
    };
}