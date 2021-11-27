// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable once PropertyCanBeMadeInitOnly.Global
// ReSharper disable once MemberCanBePrivate.Global
namespace Playground.PaymentEngine.Store.EF.Accounts;

public class AccountMetaData
{
    public AccountMetaData() => Name = null!;

    public long Id { get; set; }
    public string Name { get; set; }
    public string? Value { get; set; }
    public long TenantId { get; set; }
    public long AccountId { get; set; }
}