namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer {
    public record GetCustomerResponse {
        public Customer? Customer { get; set; }
    }
}