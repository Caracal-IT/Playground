using System.Xml.Serialization;

namespace Playground.PaymentEngine.UseCases.Shared {
    [XmlRoot(ElementName="meta-data")]
    public class MetaData {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; } = string.Empty;
    }
}