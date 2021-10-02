namespace Playground.PaymentEngine.Api.Models.Shared;

public record MetaData: Entity {
    public string Name { get; set; }
    public string Value { get; set; }
}