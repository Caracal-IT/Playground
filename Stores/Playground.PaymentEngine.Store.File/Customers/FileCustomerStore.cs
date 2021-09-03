using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Customers;
using Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Store.File.Customers {
    public class FileCustomerStore : FileStore, CustomerStore {
        private readonly CustomerData _data;

        public FileCustomerStore() => _data = GetRepository<CustomerData>();

        public Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken cancellationToken) =>
            Task.FromResult(_data.Customers.AsEnumerable());
        
        public async Task<IEnumerable<Customer>> GetCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken) {
            var customers = await GetCustomersAsync(cancellationToken);
            return customers.Where(i => customerIds.Contains(i.Id));
        }

        private static readonly object CreateLock = new();
        public Task<Customer> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken) {
            long id;
            
            lock (CreateLock) {
                id = _data.Customers.Any() ? _data.Customers.Max(w => w.Id) + 1: 1;
            }

            customer.Id = id;
            _data.Customers.Add(customer);
            
            return Task.FromResult(customer);
        }
        
        public Task DeleteCustomersAsync(IEnumerable<long> customerIds, CancellationToken cancellationToken) {
            _data.Customers = _data.Customers.Where(c => !customerIds.Contains(c.Id)).ToList();
            return Task.CompletedTask;
        }

        public async Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken) {
            await DeleteCustomersAsync(new[] { customer.Id }, cancellationToken);
            _data.Customers.Add(customer);
        }
    }
}