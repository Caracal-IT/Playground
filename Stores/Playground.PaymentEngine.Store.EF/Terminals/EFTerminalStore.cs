// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Terminals; 

public partial class EFTerminalStore: DbContext, TerminalStore {
    private DbSet<Terminal> Terminals { get; set; } = null!;
    private DbSet<TerminalMap> TerminalMaps { get; set; } = null!;
    private DbSet<TerminalResult> TerminalResults { get; set; } = null!;
    
    public async Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken) =>
        await TerminalMaps
            .Join(Terminals.Include(t => t.Settings),
                tm => tm.TerminalId,
                t => t.Id,
                (tm, t) => new {Map = tm, Terminal = t}
            )
            .Where(t => t.Map.Enabled && t.Map.AccountTypeId == accountTypeId)
            .OrderBy(t => t.Map.Order)
            .Select(t => t.Terminal)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<Terminal>> GetTerminalsAsync(CancellationToken cancellationToken) => 
        await Terminals.Include(t => t.Settings).ToListAsync(cancellationToken);

    public async Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken) {
        await TerminalResults.AddRangeAsync(results, cancellationToken);
        await SaveChangesAsync(cancellationToken);
    }
}