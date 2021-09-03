using Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer {
    public class Customer {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}