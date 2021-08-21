using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="rules-histories")]
    public class RuleHistories {
        [XmlElement(ElementName="rule-history")]
        public List<RuleHistory> Histories { get; set; } = new();
    }
}