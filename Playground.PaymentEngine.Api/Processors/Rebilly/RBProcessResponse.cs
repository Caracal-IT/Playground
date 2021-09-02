using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Api.Processors.Rebilly {
    public class RBProcessResponse {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
        
        [JsonPropertyName("trans-ref")]
        public string Reference { get; set; }
        
        [JsonPropertyName("code")]
        public string Code { get; set; }
        
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
    }
}