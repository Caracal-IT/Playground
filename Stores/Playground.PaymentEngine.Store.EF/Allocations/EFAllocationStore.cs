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
    
    public async Task SetAllocationStatusAsync(long id, long statusId, CancellationToken cancellationToken) {
        var allocation = await Allocations.FirstOrDefaultAsync(cancellationToken);
        
        if(allocation == null)
            return;
        
        allocation.AllocationStatusId = statusId;

        Allocations.Update(allocation);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SetAllocationStatusAsync(long id, long statusId, string terminal, CancellationToken cancellationToken) {
        var allocation = await Allocations.FirstOrDefaultAsync(cancellationToken);
        
        if(allocation == null)
            return;
        
        allocation.AllocationStatusId = statusId;
        allocation.Terminal = terminal;
        
        Allocations.Update(allocation);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task SetAllocationStatusAsync(long id, long statusId, string terminal, string? reference, CancellationToken cancellationToken) {
        var allocation = await Allocations.FirstOrDefaultAsync(cancellationToken);
        
        if(allocation == null)
            return;
        
        allocation.AllocationStatusId = statusId;
        allocation.Terminal = terminal;
        allocation.Reference = reference;
        
        Allocations.Update(allocation);
        await SaveChangesAsync(cancellationToken);
    }

    public async Task<Allocation> SaveAllocationAsync(Allocation allocation, CancellationToken cancellationToken) {
        Allocation dbAllocation;

        if (await Allocations.AnyAsync(a => a.Id == allocation.Id, cancellationToken)) 
            dbAllocation = Allocations.Update(allocation).Entity;
        else {
            allocation.AllocationStatusId = 2;
            dbAllocation = Allocations.Add(allocation).Entity;
        }
        
        await SaveChangesAsync(cancellationToken);
        return dbAllocation;
    }

    public async Task RemoveAllocationsAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
        var allocations = await Allocations.Where(a => a.WithdrawalGroupId == withdrawalGroupId)
                                           .ToListAsync(cancellationToken);
        
        Allocations.RemoveRange(allocations);
        await SaveChangesAsync(cancellationToken);
    }

    public AllocationStore Clone() {
        return new EFAllocationStore();
    }
}