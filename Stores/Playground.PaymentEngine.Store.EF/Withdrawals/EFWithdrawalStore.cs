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
        var groupedWithdrawals = WithdrawalGroups.SelectMany(g => g.WithdrawalIds);
        var ids = withdrawalIds.Where(i => !groupedWithdrawals.Contains(i));
        var withdrawals = await GetWithdrawalsAsync(ids, cancellationToken);

        var groups = withdrawals.AsEnumerable().GroupBy(w => w.CustomerId, w => w.Id, CreateWithdrawalGroup).ToList();

        await WithdrawalGroups.AddRangeAsync(groups, cancellationToken);
        return groups;

        WithdrawalGroup CreateWithdrawalGroup(long customerId, IEnumerable<long> Ids) =>
            new() { CustomerId = customerId, WithdrawalIdsString = string.Join(",", Ids)};
    }
    
    public Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task UnGroupWithdrawalsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<WithdrawalGroup?> AppendWithdrawalGroupsAsync(long Id, IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }
}