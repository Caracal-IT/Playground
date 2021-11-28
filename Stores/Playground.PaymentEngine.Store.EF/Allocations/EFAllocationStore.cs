// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Allocations; 

public partial class EFAllocationStore: DbContext, AllocationStore {
    private DbSet<Allocation> Allocations { get; set; } = null!;

    public async Task<IEnumerable<Allocation>> GetAllocationsAsync(CancellationToken cancellationToken) =>
        await Allocations.ToListAsync(cancellationToken);

    public async Task<IEnumerable<Allocation>> GetAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken) =>
        await Allocations.Where(a => allocationIds.Contains(a.Id))
                         .ToListAsync(cancellationToken);

    public async Task DeleteAllocationsAsync(IEnumerable<long> allocationIds, CancellationToken cancellationToken) {
        var allocations = await Allocations.Where(c => allocationIds.Contains(c.Id))
                                           .ToListAsync(cancellationToken);
        
        Allocations.RemoveRange(allocations);
        
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Allocation>> GetAllocationsByReferenceAsync(string reference, CancellationToken cancellationToken) =>
        await Allocations.Where(a => a.Reference == reference)
                         .ToListAsync(cancellationToken);
    
    public Task SetAllocationStatusAsync(long id, long statusId, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task SetAllocationStatusAsync(long id, long statusId, string terminal, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task SetAllocationStatusAsync(long id, long statusId, string terminal, string? reference,
        CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<Allocation> SaveAllocationAsync(Allocation allocation, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }
}