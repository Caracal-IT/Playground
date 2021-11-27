// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.Accounts;

public partial class EFAccountStore: DbContext, AccountStore
{
    private DbSet<Account> Accounts { get; set; } = null!;
    private DbSet<AccountType> AccountTypes { get; set; } = null!;

    public IQueryable<Account> GetAccounts() =>
        Accounts.Include(a => a.MetaData);
    
    public async Task<Account> GetAccountAsync(long id, CancellationToken cancellationToken) =>
        await Accounts
                .Include(a => a.MetaData)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync(cancellationToken)
                ??new Account{Id = id};

    public async Task<IEnumerable<Account>> GetCustomerAccountsAsync(long id, CancellationToken cancellationToken) =>
        await Accounts
                .Include(a => a.MetaData)
                .Where(a => a.CustomerId == id)
                .ToListAsync(cancellationToken);

    public async Task<IEnumerable<AccountType>> GetAccountTypesAsync(IEnumerable<long> accountTypeIds, CancellationToken cancellationToken) =>
        await AccountTypes
                .Where(a => accountTypeIds.Contains(a.Id))
                .ToListAsync(cancellationToken);
}