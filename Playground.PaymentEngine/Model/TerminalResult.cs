using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="terminal-result")]
    public class TerminalResult {
        [XmlElement("meta-data")]
        public List<MetaData> MetaData { get; set; }
        [XmlAttribute("success")]
        public bool Success { get; set; }
        [XmlAttribute("date")]
        public DateTime Date { get; set; }
        [XmlAttribute("reference")]
        public string Reference { get; set; }
        [XmlAttribute("terminal")]
        public string Terminal { get; set; }
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlElement("message")]
        public string Message { get; set; }
    }
}