namespace Playground.PaymentEngine.Api.Models.Payments;

public record ProcessRequest {
    public bool Consolidate { get; set; } = false;
    // ReSharper disable once CollectionNeverUpdated.Global
    public List<long> Allocations { get; set; } = new();
}