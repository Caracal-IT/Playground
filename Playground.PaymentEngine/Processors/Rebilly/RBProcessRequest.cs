using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.Rebilly {
    public class RBProcessRequest {
        [JsonPropertyName("trans-ref")]
        public string Reference { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        
        [JsonPropertyName("card-holder")]
        public string CardHolder { get; set; }
        
        [JsonPropertyName("hash")]
        public string Hash { get; set; }
        
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}