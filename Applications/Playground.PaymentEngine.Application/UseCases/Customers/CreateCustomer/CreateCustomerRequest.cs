namespace Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;

public record CreateCustomerRequest(decimal Balance) {
    public List<MetaData> MetaData { get; set; } = new();
}