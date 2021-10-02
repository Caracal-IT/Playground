namespace Playground.Core.Data;

public class Entity {
    [XmlAttribute("id")] public long Id { get; set; }
    [XmlAttribute("tenant-id")] public long TenantId { get; set; } = 1;
}