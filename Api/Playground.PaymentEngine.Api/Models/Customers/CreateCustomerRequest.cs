namespace Playground.PaymentEngine.Api.Models.Customers;

public record CreateCustomerRequest(decimal Balance) {
    public List<MetaData> MetaData { get; set; } = new();
}