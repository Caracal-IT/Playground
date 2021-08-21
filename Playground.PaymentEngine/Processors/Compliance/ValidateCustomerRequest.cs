namespace Playground.PaymentEngine.Processors.Compliance {
    public class ValidateCustomerRequest {
        public long CustomerId { get; set; }
        public decimal Amount { get; set; }
    }
}