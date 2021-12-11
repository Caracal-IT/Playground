namespace Playground.PaymentEngine.Store.File.Allocations;

using Playground.PaymentEngine.Store.Allocations;
using Playground.PaymentEngine.Store.Allocations.Model;

public class FileAllocationStore : FileStore, AllocationStore {
    private readonly AllocationData _data;

    private readonly object _allocationLock = new();

    public FileAllocationStore() =>
        _data = GetRepository<AllocationData>();

    public Task<IEnumerable<Allocation>> GetAllocationsAsync(CancellationToken cancellationToken) =>
        Task.FromResult(_data.Allocations.AsEnumerable());

    public async Task<IEnumerable<Allocation>> GetAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken) {
        var result = await GetAllocationsAsync(cancellationToken);
        return result.Where(a => allocationIds.Contains(a.Id));
    }

    public Task DeleteAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken) {
        _data.Allocations = _data.Allocations.Where(d => !allocationIds.Contains(d.Id)).ToList();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Allocation>> GetAllocationsByReferenceAsync(string reference, CancellationToken cancellationToken) {
        var result = _data.Allocations
            .Where(a => !string.IsNullOrWhiteSpace(a.Terminal) && (a.Reference?.Equals(reference) ?? false));

        return Task.FromResult(result);
    }

    public Task SetAllocationStatusAsync(long id, long statusId, CancellationToken cancellationToken) =>
        SetAllocationStatusAsync(id, statusId, null, cancellationToken);

    public Task SetAllocationStatusAsync(long id, long statusId, string? terminal, CancellationToken cancellationToken) =>
        SetAllocationStatusAsync(id, statusId, terminal, null, cancellationToken);

    public async Task SetAllocationStatusAsync(long id, long statusId, string? terminal, string? reference, CancellationToken cancellationToken) {
        var allocations = await GetAllocationsAsync(new[] { id }, cancellationToken);
        var allocation = allocations.FirstOrDefault() ?? new Allocation();

        allocation.AllocationStatusId = statusId;
        allocation.Terminal = terminal;
        allocation.Reference = reference;
    }

    public async Task<Allocation> SaveAllocationAsync(Allocation allocation, CancellationToken cancellationToken) {
        var allocations = await GetAllocationsAsync(new[] { allocation.Id }, cancellationToken);
        var alloc = allocations.FirstOrDefault();

        if (alloc == null) {
            lock (_allocationLock) {
                allocation.Id = _data.Allocations.LastOrDefault()?.Id ?? 0 + 1;
            }
        }
        else
            _data.Allocations.Remove(alloc);

        _data.Allocations.Add(allocation);

        return allocation;
    }

    public Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        var allocations = _data.Allocations;

        _data
            .Allocations
            .Where(a => a.WithdrawalGroupId == withdrawalGroupId)
            .ToList()
            .ForEach(a => allocations.Remove(a));

        return Task.CompletedTask;
    }

    public AllocationStore Clone() {
        return this;
    }
}