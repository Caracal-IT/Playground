using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="terminal")]
    public class Terminal {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName="retry-count")]
        public int RetryCount { get; set; }
    }
}