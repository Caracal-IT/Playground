using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Model {
    [XmlRoot(ElementName="meta-data")]
    public class MetaData {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; } = string.Empty;
    }
}