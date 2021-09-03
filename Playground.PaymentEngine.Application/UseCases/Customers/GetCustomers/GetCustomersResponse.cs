using System;

namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers {
    public class GetCustomersResponse {
        public IEnumerable<Customer> Customers { get; set; } = Array.Empty<Customer>();
    }
}