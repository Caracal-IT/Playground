using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.Rebilly {
    public class RBCallbackRequest {
        [JsonPropertyName("reference")]
        public string Reference { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }
    }
}