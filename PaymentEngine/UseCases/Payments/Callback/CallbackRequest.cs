namespace PaymentEngine.UseCases.Payments.Callback {
    public class CallbackRequest {
        public string Action { get; set; }
        public string Data { get; set; }
        public string Reference { get; set; }
    }
}