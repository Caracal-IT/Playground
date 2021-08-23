using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.Accounts.Model;

namespace Playground.PaymentEngine.Stores.Accounts.File {
    public class FileAccountStore: AccountStore {
        private readonly AccountRepository _repository;
        private readonly ICacheService _cacheService;
        
        public FileAccountStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _repository = GetRepository();
        }
        
        public Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken) {
            var result = _cacheService.GetValue(
                $"{nameof(GetAccountAsync)}_{id}", 
                () => _repository.Accounts.FirstOrDefault(a => a.Id == id) ?? new Account());

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken) {
            var result = _cacheService.GetValue(
                $"{nameof(GetCustomerAccountsAsync)}_{id}", 
                () => _repository.Accounts.Where(a => a.CustomerId == id));

            return Task.FromResult(result);
        }

        public Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken) {
            var result = _cacheService.GetValue(
                $"{nameof(GetAccountTypesAsync)}_{accountTypeIds}",
                () => _repository.AccountTypes.Where(t => accountTypeIds.Contains(t.Id)));

            return Task.FromResult(result);
        }

        private static AccountRepository GetRepository() {
            var path = Path.Join("Stores", "Accounts", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (AccountRepository) new XmlSerializer(typeof(AccountRepository)).Deserialize(fileStream);
        }
    }
}