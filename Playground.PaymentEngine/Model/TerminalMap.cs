using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="terminal-map")]
    public class TerminalMap {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="account-type-id")]
        public long AccountTypeId { get; set; }
        [XmlAttribute(AttributeName="terminal-id")]
        public long TerminalId { get; set; }
        [XmlAttribute(AttributeName="order")]
        public short Order { get; set; }
        [XmlAttribute(AttributeName="enabled")]
        public bool Enabled { get; set; }
    }
}