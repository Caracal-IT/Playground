using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Model {
    [XmlRoot("meta-data")]
    public class MetaData {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute("value")]
        public string Value { get; set; } = string.Empty;
    }
}