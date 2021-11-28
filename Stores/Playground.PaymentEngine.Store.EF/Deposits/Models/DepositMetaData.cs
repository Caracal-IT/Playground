namespace Playground.PaymentEngine.Store.EF.Deposits.Models; 

public class DepositMetaData {
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public long TenantId { get; set; }
    public long DepositId { get; set; }
}