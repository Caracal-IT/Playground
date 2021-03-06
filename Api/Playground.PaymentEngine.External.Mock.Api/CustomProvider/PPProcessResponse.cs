namespace Playground.PaymentEngine.External.Mock.Api.CustomProvider;

[XmlRoot("response")]
public class PPProcessResponse {
    [XmlElement("name")]
    [JsonPropertyName("name")] 
    public string Name { get; set; }
    
    [XmlAttribute("reference")]
    [JsonPropertyName("reference")]
    public string Reference { get; set; }
    
    [XmlElement("code")]
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
    [XmlElement("amount")]
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}