using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Stores.Terminals.Model;

namespace Playground.PaymentEngine.Stores.Terminals.File {
    [XmlRoot("repository")]
    public class TerminalData {
        [XmlArray("terminals")]
        [XmlArrayItem("terminal")]
        public List<Terminal> Terminals { get; set; }
		
        [XmlArray("terminal-maps")]
        [XmlArrayItem("terminal-map")]
        public List<TerminalMap> TerminalMaps { get; set; }
		
        [XmlArray("terminal-results")]
        [XmlArrayItem("terminal-result")]
        public List<TerminalResult> TerminalResults { get; set; }
    }
}