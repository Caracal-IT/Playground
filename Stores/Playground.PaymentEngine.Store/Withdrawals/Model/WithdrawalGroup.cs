using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Store.Withdrawals.Model {
    [XmlRoot("withdrawal-group")]
    public class WithdrawalGroup {
        [XmlAttribute("id")]
        public long Id { get; set; }
        
        [XmlAttribute("customer-id")]
        public long CustomerId { get; set; }

        [XmlAttribute("withdrawal-ids")]
        public string WithdrawalIdsString {
            get => string.Join(",", WithdrawalIds);
            set => WithdrawalIds = value.Split(',').Select(x => Convert.ToInt64(x!.Trim())).ToList();
        }

        [XmlIgnore]
        public List<long> WithdrawalIds { get; set; } = new();
    }
}