using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Customers;
using Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Store.File.Customers {
    public class FileCustomerStore : FileStore, CustomerStore {
        private readonly CustomerData _data;

        public FileCustomerStore() => _data = GetRepository<CustomerData>();

        public Task<Customer?> GetCustomerAsync(long id, CancellationToken cancellationToken) {
            var result = _data.Customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(result);
        }
    }
}