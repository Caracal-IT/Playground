// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Deposits; 

public partial class EFDepositStore: DbContext, DepositStore {
    private DbSet<Deposit> Deposits { get; set; } = null!;
    private DbSet<MetaData> MetaData { get; set; } = null!;
    
    public async Task<IEnumerable<Deposit>> GetDepositsAsync(CancellationToken cancellationToken) => 
        await Deposits.Include(d => d.MetaData).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Deposit>> GetDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken) => 
        await Deposits.Include(d => d.MetaData).Where(d => depositIds.Contains(d.Id)).ToListAsync(cancellationToken);

    public async Task<Deposit> CreateDepositAsync(Deposit deposit, CancellationToken cancellationToken) {
        var d = await Deposits.AddAsync(deposit, cancellationToken);
        d.State = EntityState.Added;
        await SaveChangesAsync(cancellationToken);
        return d.Entity;
    }

    public async Task DeleteDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken) {
        var deposits = await Deposits
            .Include(d=> d.MetaData)
            .Where(c => depositIds.Contains(c.Id))
            .ToListAsync(cancellationToken);
        
        MetaData.RemoveRange(deposits.SelectMany(c => c.MetaData));
        Deposits.RemoveRange(deposits);
        await SaveChangesAsync(cancellationToken);
    }
}