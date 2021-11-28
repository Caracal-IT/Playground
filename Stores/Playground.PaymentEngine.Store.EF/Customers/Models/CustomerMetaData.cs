namespace Playground.PaymentEngine.Store.EF.Customers.Models; 

public class CustomerMetaData {
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Value { get; set; }
    public long TenantId { get; set; }
    public long CustomerId { get; set; }
}