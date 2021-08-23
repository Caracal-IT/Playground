using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Customers.Model;

namespace Playground.PaymentEngine.Stores.Customers.File {
    public class FileCustomerStore : CustomerStore {
        private CustomerRepository _repository;

        public FileCustomerStore() => _repository = GetRepository();

        public Customer GetCustomer(long id) =>
            _repository.Customers.FirstOrDefault(c => c.Id == id);
        
        private static CustomerRepository GetRepository() {
            var path = Path.Join("Stores", "Customers", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (CustomerRepository) new XmlSerializer(typeof(CustomerRepository)).Deserialize(fileStream);
        }
    }
}