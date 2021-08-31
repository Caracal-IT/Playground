namespace Playground.PaymentEngine.Application.UseCases.Payments.Callback {
    public class CallbackRequest {
        public string Action { get; set; } = string.Empty;
        public string Data { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
    }
}