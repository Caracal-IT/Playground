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

    private static void CreateModel(ModelBuilder modelBuilder) { }

    private static void CreateDefaults(ModelBuilder modelBuilder) { }
}

/*
 <repository>    
    <approval-rule-history withdrawal-group-id="-2" transaction-id="8c5c1ae6-b2ab-4715-aebe-258a64aee52d" transaction-date="2020-01-11">
        <approval-rule name="rule1" isSuccessful="true" message="message"/>
        <approval-rule name="rule2" isSuccessful="true" message="message"/>
        <meta-data name="withdrawal-id" value="-1"/>
    </approval-rule-history>    
</repository>
*/