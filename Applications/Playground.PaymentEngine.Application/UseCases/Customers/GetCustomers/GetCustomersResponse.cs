namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;

public record GetCustomersResponse {
    public IEnumerable<Customer> Customers { get; set; } = Array.Empty<Customer>();
}