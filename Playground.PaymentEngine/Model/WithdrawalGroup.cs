using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="withdrawal-group")]
    public class WithdrawalGroup {
        [XmlAttribute(AttributeName="id")]
        public long Id { get; set; }
        
        [XmlAttribute(AttributeName="customer-id")]
        public long CustomerId { get; set; }

        [XmlAttribute(AttributeName = "withdrawal-ids")]
        public string WithdrawalIdsString {
            get => string.Join(",", WithdrawalIds);
            set => WithdrawalIds = value.Split(',').Select(x => Convert.ToInt64(x!.Trim())).ToList();
        }

        [XmlIgnore]
        public List<long> WithdrawalIds { get; set; } = new();
    }
}