using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Store.Customers {
    public interface CustomerStore {
        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken);
        Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task DeleteCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken);
        Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    }
}