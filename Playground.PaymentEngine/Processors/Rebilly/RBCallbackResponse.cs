using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.Rebilly {
    public class RBCallbackResponse {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
        
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}