using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers.File {
    public class FileCustomerStore : FileStore, CustomerStore {
        private readonly CustomerRepository _repository;

        public FileCustomerStore() => _repository = GetRepository<CustomerRepository>();

        public Task<Customer> GetCustomerAsync(long id, CancellationToken cancellationToken) {
            var result = _repository.Customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(result);
        }
    }
}