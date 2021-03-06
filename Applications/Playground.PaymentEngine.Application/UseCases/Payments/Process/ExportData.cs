namespace Playground.PaymentEngine.Application.UseCases.Payments.Process;

using System.Xml.Serialization;

[XmlRoot(ElementName = "export-data")]
public record ExportData {
    [XmlIgnore] public List<ExportAllocation> Allocations { get; set; } = new();
    [XmlIgnore] public List<ExportResponse> Response { get; set; } = new();

    [XmlAttribute(AttributeName = "reference")]
    public string? Reference { get; set; }

    [XmlAttribute(AttributeName = "amount", DataType = "decimal")]
    public decimal Amount { get; set; }

    [XmlAttribute(AttributeName = "account-type-id")]
    public long AccountTypeId { get; set; }

    [XmlElement(ElementName = "meta-data")]
    public List<MetaData> MetaData { get; set; } = new();
}