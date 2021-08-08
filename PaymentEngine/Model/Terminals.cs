using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="terminals")]
    public class Terminals {
        [XmlElement(ElementName="terminal")]
        public List<Terminal> TerminalList { get; set; }
    }
}