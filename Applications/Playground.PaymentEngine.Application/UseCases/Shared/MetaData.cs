namespace Playground.PaymentEngine.Application.UseCases.Shared;

using System.Xml.Serialization;
using Core.Model;

[XmlRoot(ElementName = "meta-data")]
public record MetaData : Entity {
    [XmlAttribute(AttributeName = "name")] public string Name { get; set; } = string.Empty;

    [XmlAttribute(AttributeName = "value")]
    public string Value { get; set; } = string.Empty;
}