using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="meta-data")]
    public class MetaData {
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName="value")]
        public string Value { get; set; }
    }
}