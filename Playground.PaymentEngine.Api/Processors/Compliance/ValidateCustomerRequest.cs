namespace Playground.PaymentEngine.Api.Processors.Compliance {
    public class ValidateCustomerRequest {
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}