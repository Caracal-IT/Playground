using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.PaymentEngine.Model {
    [XmlRoot(ElementName="terminal-results")]
    public class TerminalResults {
        [XmlElement(ElementName="terminal-result")]
        public List<TerminalResult> TerminalResultList { get; set; }
    }
}