using System.Collections.Generic;
using System.Xml.Serialization;

namespace PaymentEngine.Model {
    [XmlRoot(ElementName="terminal-maps")]
    public class TerminalMaps {
        [XmlElement(ElementName="terminal-map")]
        public List<TerminalMap> TerminalMapList { get; set; }
    }
}