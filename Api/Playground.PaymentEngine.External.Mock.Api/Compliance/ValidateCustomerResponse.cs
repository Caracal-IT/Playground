namespace Playground.PaymentEngine.External.Mock.Api.Compliance;

public class ValidateCustomerResponse {
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }
}