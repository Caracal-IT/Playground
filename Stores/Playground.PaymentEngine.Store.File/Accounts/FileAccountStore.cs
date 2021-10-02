namespace Playground.PaymentEngine.Store.File.Accounts;

using Playground.PaymentEngine.Store.Accounts;
using Playground.PaymentEngine.Store.Accounts.Model;

public class FileAccountStore : FileStore, AccountStore {
    private readonly AccountData _data;
    private readonly ICacheService _cacheService;

    public FileAccountStore(ICacheService cacheService) {
        _cacheService = cacheService;
        _data = GetRepository<AccountData>();
    }

    public Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken) {
        var result = _cacheService.GetValue(
            $"{nameof(GetAccountAsync)}_{id}",
            () => _data.Accounts.FirstOrDefault(a => a.Id == id) ?? new Account());

        return Task.FromResult(result);
    }

    public Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken) {
        var result = _cacheService.GetValue(
            $"{nameof(GetCustomerAccountsAsync)}_{id}",
            () => _data.Accounts.Where(a => a.CustomerId == id));

        return Task.FromResult(result);
    }

    public Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken) {
        var result = _cacheService.GetValue(
            $"{nameof(GetAccountTypesAsync)}_{accountTypeIds}",
            () => _data.AccountTypes.Where(t => accountTypeIds.Contains(t.Id)));

        return Task.FromResult(result);
    }
}