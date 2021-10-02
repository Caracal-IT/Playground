namespace Playground.PaymentEngine.External.Mock.Api.Rebilly;

public class RBCallbackResponse {
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("reference")]
    public string Reference { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; }
}