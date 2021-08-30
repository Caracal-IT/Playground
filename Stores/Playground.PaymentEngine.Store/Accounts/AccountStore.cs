using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Accounts.Model;

namespace Playground.PaymentEngine.Store.Accounts {
    public interface AccountStore {
        Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken);
    }
}