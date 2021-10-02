namespace Playground.PaymentEngine.Store.File.Withdrawals;

using Playground.PaymentEngine.Store.Withdrawals;
using Playground.PaymentEngine.Store.Withdrawals.Model;

public class FileWithdrawalStore : FileStore, WithdrawalStore {
    private readonly WithdrawalData _data;

    public FileWithdrawalStore() =>
        _data = GetRepository<WithdrawalData>();

    private static readonly object CreateLock = new();

    public Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken) {
        long id;

        lock (CreateLock) {
            id = _data.Withdrawals.Any() ? _data.Withdrawals.Max(w => w.Id) + 1 : 1;
        }

        withdrawal.Id = id;
        _data.Withdrawals.Add(withdrawal);

        return Task.FromResult(withdrawal);
    }

    public Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(CancellationToken cancellationToken) =>
        Task.FromResult(_data.Withdrawals.Where(w => !w.IsDeleted).AsQueryable());

    public async Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var result = await GetWithdrawalsAsync(cancellationToken);
        return result.Where(w => withdrawalIds.Contains(w.Id)).AsQueryable();
    }

    public async Task DeleteWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var w = await GetWithdrawalsAsync(withdrawalIds, cancellationToken);
        var withdrawals = w.ToList();

        withdrawals.ForEach(i => i.IsDeleted = true);
        _data.Withdrawals = _data.Withdrawals
            .Where(i => !i.IsDeleted && i.WithdrawalStatusId != 0)
            .ToList();
    }

    public async Task UpdateWithdrawalStatusAsync(IEnumerable<long> withdrawalIds, long statusId, CancellationToken cancellationToken) {
        if (statusId == 0)
            return;

        var withdrawals = await GetWithdrawalsAsync(withdrawalIds, cancellationToken);
        withdrawals.ToList().ForEach(w => w.WithdrawalStatusId = statusId);
    }

    public async Task<IEnumerable<WithdrawalGroup>> GroupWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var groupedWithdrawals = _data.WithdrawalGroups.SelectMany(g => g.WithdrawalIds);
        var ids = withdrawalIds.Where(i => !groupedWithdrawals.Contains(i));
        var withdrawals = await GetWithdrawalsAsync(ids, cancellationToken);

        var groups = withdrawals.GroupBy(w => w.CustomerId, w => w.Id, CreateWithdrawalGroup).ToList();

        _data.WithdrawalGroups.AddRange(groups);
        return groups;
    }

    private WithdrawalGroup CreateWithdrawalGroup(long customerId, IEnumerable<long> withdrawalIds) =>
        new() {
            Id = GetNewGroupId(),
            CustomerId = customerId,
            WithdrawalIdsString = string.Join(",", withdrawalIds)
        };

    private static readonly object GroupLock = new();

    private long GetNewGroupId() {
        lock (GroupLock) {
            return _data.WithdrawalGroups.Max(w => w.Id) + 1;
        }
    }

    public Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken) {
        var result = _data.WithdrawalGroups.FirstOrDefault(g => g.Id == id);

        return Task.FromResult(result ?? new WithdrawalGroup());
    }

    public Task UnGroupWithdrawalsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        _data.WithdrawalGroups = _data.WithdrawalGroups.Where(g => g.Id != withdrawalGroupId).ToList();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(CancellationToken cancellationToken) {
        return Task.FromResult(_data.WithdrawalGroups.AsEnumerable());
    }

    public Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) {
        var result = _data.WithdrawalGroups
            .Where(g => withdrawalGroupIds.Contains(g.Id));

        return Task.FromResult(result);
    }

    public async Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken) {
        var group = _data.WithdrawalGroups.FirstOrDefault(g => g.Id == id) ?? new WithdrawalGroup();
        return await GetWithdrawalsAsync(group.WithdrawalIds, cancellationToken);
    }

    public async Task<WithdrawalGroup?> AppendWithdrawalGroupsAsync(long id, IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
        var groups = await GetWithdrawalGroupsAsync(new[] { id }, cancellationToken);
        var group = groups.FirstOrDefault();

        if (group == null) return null;

        var groupedWithdrawals = _data.WithdrawalGroups.SelectMany(g => g.WithdrawalIds);
        var ids = withdrawalIds.Where(i => !groupedWithdrawals.Contains(i));

        var wIds = group.WithdrawalIds;
        wIds.AddRange(ids);
        group.WithdrawalIdsString = string.Join(",", wIds.Distinct());

        return group;
    }
}