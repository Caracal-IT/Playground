namespace Playground.PaymentEngine.External.Mock.Api.Rebilly;

public class RBCallbackRequest {
    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}