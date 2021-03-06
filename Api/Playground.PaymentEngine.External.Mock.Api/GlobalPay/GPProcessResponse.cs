 namespace Playground.PaymentEngine.External.Mock.Api.GlobalPay;
 
public class GPProcessResponse {
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [JsonPropertyName("reference")]
    public string Reference { get; set; }
    
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}