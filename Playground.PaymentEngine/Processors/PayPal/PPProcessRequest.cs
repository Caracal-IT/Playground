using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.PayPal {
    public class PPProcessRequest {
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
        
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}