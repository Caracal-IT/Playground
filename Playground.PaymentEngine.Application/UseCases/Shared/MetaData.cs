using System.Xml.Serialization;

namespace Playground.PaymentEngine.Application.UseCases.Shared {
    [XmlRoot(ElementName="meta-data")]
    public record MetaData {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; } = string.Empty;
    }
}