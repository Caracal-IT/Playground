using System.Collections.Generic;
using Playground.PaymentEngine.Stores.Accounts.Model;

namespace Playground.PaymentEngine.Stores.Accounts {
    public interface AccountStore {
        Account GetAccount(long id);
        IEnumerable<Account> GetCustomerAccounts(long id);
        IEnumerable<AccountType> GetAccountTypes(IEnumerable<long> accountTypeIds);
    }
}