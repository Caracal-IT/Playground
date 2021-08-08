using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="account")]
    public class Account {
        [XmlElement(ElementName="meta-data")]
        public List<MetaData> MetaData { get; set; }
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute(AttributeName="account-type-id")]
        public long AccountTypeId { get; set; }
        [XmlAttribute(AttributeName="customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute(AttributeName="exposure", DataType="decimal")]
        public decimal Exposure { get; set; }
    }
}