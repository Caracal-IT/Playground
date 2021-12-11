namespace Playground.PaymentEngine.Store.Customers;

using Model;

public interface CustomerStore {
    Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken);
    Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    Task DeleteCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken);
    Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    CustomerStore Clone();
}