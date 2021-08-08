using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="allocation-status")]
    public class AllocationStatus {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="name")]
        public string Name { get; set; }
    }
}