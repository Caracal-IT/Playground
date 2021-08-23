using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Accounts.Model;

namespace Playground.PaymentEngine.Stores.Accounts {
    public interface AccountStore {
        Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken);
    }
}