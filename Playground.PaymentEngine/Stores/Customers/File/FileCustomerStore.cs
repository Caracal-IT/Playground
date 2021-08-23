using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers.File {
    public class FileCustomerStore : CustomerStore {
        private readonly CustomerRepository _repository;

        public FileCustomerStore() => _repository = GetRepository();

        public Task<Customer> GetCustomerAsync(long id, CancellationToken cancellationToken) {
            var result = _repository.Customers.FirstOrDefault(c => c.Id == id);
            return Task.FromResult(result);
        }

        private static CustomerRepository GetRepository() {
            var path = Path.Join("Stores", "Customers", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (CustomerRepository) new XmlSerializer(typeof(CustomerRepository)).Deserialize(fileStream);
        }
    }
}