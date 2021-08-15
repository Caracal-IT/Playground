using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.GlobalPay {
    public class GPProcessRequest {
        [JsonPropertyName("reference")]
        public string Reference { get; set; }
        
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}