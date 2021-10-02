namespace Playground.Core.Model;

public record Entity {
    public long Id { get; set; }
    public long TenantId { get; set; } = 1;
}