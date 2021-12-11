// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Withdrawals; 

public partial class EFWithdrawalStore: DbContext, WithdrawalStore {
    private DbSet<Withdrawal> Withdrawals { get; set; } = null!;
    private DbSet<WithdrawalGroup> WithdrawalGroups { get; set; } = null!;
    
    public async Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken) {
        var w = Withdrawals.Add(withdrawal);
        w.State = EntityState.Added;
        await SaveChangesAsync(cancellationToken);
        return w.Entity;
    }
    
    public async Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(CancellationToken cancellationToken) => 
        (await Withdrawals.ToListAsync(cancellationToken)).AsQueryable();

    public async Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) =>
        (await Withdrawals.Where(w => withdrawalIds.Contains(w.Id)).ToListAsync(cancellationToken)).AsQueryable();
    
    public async Task DeleteWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var withdrawals = Withdrawals.Where(w => withdrawalIds.Contains(w.Id));
        Withdrawals.RemoveRange(withdrawals);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateWithdrawalStatusAsync(IEnumerable<long> withdrawalIds, long statusId, CancellationToken cancellationToken) {
        var withdrawals = await Withdrawals.Where(w => withdrawalIds.Contains(w.Id)).ToListAsync(cancellationToken);
        withdrawals.ForEach(w => w.WithdrawalStatusId = statusId);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<WithdrawalGroup>> GroupWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var withdrawalGroups = await WithdrawalGroups.ToListAsync(cancellationToken);
        var groupedWithdrawals = withdrawalGroups.SelectMany(g => g.WithdrawalIds);
        var ids = withdrawalIds.Where(i => !groupedWithdrawals.Contains(i));
        var withdrawals = await GetWithdrawalsAsync(ids, cancellationToken);

        var groups = withdrawals.AsEnumerable().GroupBy(w => w.CustomerId, w => w.Id, CreateWithdrawalGroup).ToList();

        await WithdrawalGroups.AddRangeAsync(groups, cancellationToken);
        await SaveChangesAsync(cancellationToken);
        
        return groups;

        WithdrawalGroup CreateWithdrawalGroup(long customerId, IEnumerable<long> Ids) =>
            new() { CustomerId = customerId, WithdrawalIdsString = string.Join(",", Ids)};
    }
    
    public async Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken) => 
        await WithdrawalGroups.FirstOrDefaultAsync(w => w.Id == id, cancellationToken)??new WithdrawalGroup();

    public async Task UnGroupWithdrawalsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        var group = await WithdrawalGroups.FirstOrDefaultAsync(w => w.Id == withdrawalGroupId, cancellationToken);
        
        if(group == null)
            return;
        
        WithdrawalGroups.Remove(group);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(CancellationToken cancellationToken) => 
        await WithdrawalGroups.ToListAsync(cancellationToken);

    public async Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) => 
        await WithdrawalGroups.Where(w => withdrawalGroupIds.Contains(w.Id)).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken) {
        var group = await WithdrawalGroups.FirstOrDefaultAsync(g => g.Id == id, cancellationToken) ?? new WithdrawalGroup();
        return await GetWithdrawalsAsync(group.WithdrawalIds, cancellationToken);
    }

    public async Task<WithdrawalGroup?> AppendWithdrawalGroupsAsync(long Id, IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var group = await WithdrawalGroups.Where(g => g.Id == Id).FirstOrDefaultAsync(cancellationToken);

        if (group == null) return null;

        var groupedWithdrawals = WithdrawalGroups.SelectMany(g => g.WithdrawalIds);
        var ids = withdrawalIds.Where(i => !groupedWithdrawals.Contains(i));
        var wIds = group.WithdrawalIds;
        
        wIds.AddRange(ids);
        group.WithdrawalIdsString = string.Join(",", wIds.Distinct());

        await SaveChangesAsync(cancellationToken);

        return group;
    }

    public WithdrawalStore Clone() {
        return new EFWithdrawalStore();
    }
}