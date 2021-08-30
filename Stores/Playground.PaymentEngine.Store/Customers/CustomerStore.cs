using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Store.Customers {
    public interface CustomerStore {
        Task<Customer> GetCustomerAsync(long id, CancellationToken cancellationToken);
    }
}