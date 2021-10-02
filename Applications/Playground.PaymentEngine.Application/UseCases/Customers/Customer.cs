namespace Playground.PaymentEngine.Application.UseCases.Customers;

using Core.Model;

public record Customer: Entity {
    public decimal Balance { get; set; }
    public List<MetaData> MetaData { get; set; } = new();
}