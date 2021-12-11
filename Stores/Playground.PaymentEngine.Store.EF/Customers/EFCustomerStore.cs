// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Customers; 

public partial class EFCustomerStore: DbContext, CustomerStore {
    private DbSet<Customer> Customers { get; set; } = null!;
    private DbSet<MetaData> MetaData { get; set; } = null!;
    
    public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken) => 
        await Customers.Include(c => c.MetaData).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken) =>
        await Customers.Include(c => c.MetaData)
                       .Where(c => customerIds.Contains(c.Id))
                       .ToListAsync(cancellationToken);

    public async Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken) {
        var newCustomer = await Customers.AddAsync(customer, cancellationToken);
        newCustomer.State = EntityState.Added;
        
        await SaveChangesAsync(cancellationToken);
        return newCustomer.Entity;
    }

    public async Task DeleteCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken) {
        var customers = await Customers.Include(c => c.MetaData)
                                       .Where(c => customerIds.Contains(c.Id))
                                       .ToListAsync(cancellationToken);
        
        MetaData.RemoveRange(customers.SelectMany(c => c.MetaData));
        Customers.RemoveRange(customers);
        
        await SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken) {
        Customers.Update(customer);
        await SaveChangesAsync(cancellationToken);
    }

    public CustomerStore Clone() {
        return new EFCustomerStore();
    }
}