using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers {
    public interface CustomerStore {
        Task<Customer> GetCustomerAsync(long id, CancellationToken cancellationToken);
    }
}