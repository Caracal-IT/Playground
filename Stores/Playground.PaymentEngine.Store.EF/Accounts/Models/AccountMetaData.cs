// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
namespace Playground.PaymentEngine.Store.EF.Accounts.Models;

public class AccountMetaData
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public long TenantId { get; set; }
    public long AccountId { get; set; }
}