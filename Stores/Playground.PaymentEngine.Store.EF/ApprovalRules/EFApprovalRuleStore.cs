// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.ApprovalRules;

public partial class EFApprovalRuleStore: DbContext, ApprovalRuleStore {
    private DbSet<ApprovalRuleHistory> ApprovalRuleHistories { get; set; } = null!;

    public async Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken) =>
        await ApprovalRuleHistories.Include(a => a.Rules)
                                   .Include(a => a.Metadata)
                                   .ToListAsync(cancellationToken);

    public async Task<IEnumerable<ApprovalRuleHistory>> GetLastRunApprovalRulesAsync(CancellationToken cancellationToken) =>
        await ApprovalRuleHistories.Include(a => a.Rules)
                                   .Include(a => a.Metadata)
                                   .GroupBy(
                                       r => r.WithdrawalGroupId, 
                                       (_, h) => h.OrderByDescending(i => i.TransactionDate)
                                                  .First())
                                   .ToListAsync(cancellationToken);
    
    public async Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
        ApprovalRuleHistories.AddRange(histories);
        await SaveChangesAsync(cancellationToken);
    }
}