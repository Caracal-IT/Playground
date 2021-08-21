using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Processors.Compliance {
    public class ValidateCustomerResponse {
        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }
    }
}