using System.Collections.Generic;
using Playground.PaymentEngine.Model;

namespace Playground.PaymentEngine.Stores.AccountStores {
    public interface AccountStore {
        Account GetAccount(long id);
        IEnumerable<Account> GetCustomerAccounts(long id);
        IEnumerable<AccountType> GetAccountTypes(IEnumerable<long> accountTypeIds);
    }
}