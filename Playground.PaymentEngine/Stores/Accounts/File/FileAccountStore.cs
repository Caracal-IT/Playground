using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.Accounts.Model;

namespace Playground.PaymentEngine.Stores.Accounts.File {
    public class FileAccountStore: AccountStore {
        private AccountRepository _repository;
        private readonly ICacheService _cacheService;
        
        public FileAccountStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _repository = GetRepository();
        }
        
        public Account GetAccount(long id) =>
            _cacheService.GetValue($"{nameof(GetAccount)}_{id}", () =>_repository.Accounts.FirstOrDefault(a => a.Id == id) ?? new Account());
        
         public IEnumerable<Account> GetCustomerAccounts(long id) =>
             _cacheService.GetValue($"{nameof(GetCustomerAccounts)}_{id}", () =>_repository.Accounts.Where(a => a.CustomerId == id));

         public IEnumerable<AccountType> GetAccountTypes(IEnumerable<long> accountTypeIds) =>
             _cacheService.GetValue($"{nameof(GetAccountTypes)}_{accountTypeIds}", () => _repository.AccountTypes.Where(t => accountTypeIds.Contains(t.Id)));
        
        private static AccountRepository GetRepository() {
            var path = Path.Join("Stores", "Accounts", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (AccountRepository) new XmlSerializer(typeof(AccountRepository)).Deserialize(fileStream);
        }
    }
}