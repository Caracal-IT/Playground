using System.Text.Json.Serialization;

namespace Playground.PaymentEngine.Api.Processors.Compliance {
    public class ValidateCustomerResponse {
        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }
    }
}