using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Playground.Core;
using Playground.Core.Data;

namespace Playground.PaymentEngine.Store.Withdrawals.Model {
    [XmlRoot("withdrawal-group")]
    public class WithdrawalGroup: Entity {
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