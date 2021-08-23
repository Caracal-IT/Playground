using System.Collections.Generic;
using System.Xml.Serialization;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores.TerminalStores.Model;

namespace Playground.PaymentEngine.Stores.TerminalStores.File {
    [XmlRoot("repository")]
    public class TerminalRepository {
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