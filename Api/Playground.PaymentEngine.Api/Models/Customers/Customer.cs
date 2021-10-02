namespace Playground.PaymentEngine.Api.Models.Customers;

public record Customer: Entity {
    public decimal Balance { get; set; }
    public List<MetaData> MetaData { get; set; } = new();
}