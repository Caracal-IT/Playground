using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.Core;
using Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Store.Accounts.Model {
    [XmlRoot("account")]
    public class Account: Entity {
        [XmlAttribute("account-type-id")]
        public long AccountTypeId { get; set; }
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }
        [XmlAttribute("exposure", DataType="decimal")]
        public decimal Exposure { get; set; }
        
        [XmlAttribute("pmop")]
        public bool IsPreferredAccount { get; set; }
        [XmlElement("meta-data")]
        public List<MetaData> MetaData { get; set; } = new();
    }
}