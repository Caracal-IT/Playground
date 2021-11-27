namespace Playground.PaymentEngine.Store.Accounts;

using Model;

public interface AccountStore: IDisposable
{
    IQueryable<Account> GetAccounts();
    Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken);
    Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken);
}