using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="account")]
    public class Account {
        [XmlElement("meta-data")]
        public List<MetaData> MetaData { get; set; }
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        [XmlAttribute("account-type-id")]
        public long AccountTypeId { get; set; }
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute("exposure", DataType="decimal")]
        public decimal Exposure { get; set; }
        
        [XmlAttribute("pmop")]
        public bool IsPreferredAccount { get; set; }
    }
}