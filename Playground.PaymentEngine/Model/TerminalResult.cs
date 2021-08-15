using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="terminal-result")]
    public class TerminalResult {
        [XmlElement(ElementName="meta-data")]
        public List<MetaData> MetaData { get; set; }
        [XmlAttribute(AttributeName="success")]
        public bool Success { get; set; }
        [XmlAttribute(AttributeName="date")]
        public DateTime Date { get; set; }
        [XmlAttribute(AttributeName="reference")]
        public string Reference { get; set; }
        [XmlAttribute(AttributeName="terminal-id")]
        public long TerminalId { get; set; }

    }
}