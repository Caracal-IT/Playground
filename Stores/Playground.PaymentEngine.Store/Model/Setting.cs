using System.Xml.Serialization;
using Playground.Core;
using Playground.Core.Data;

namespace Playground.PaymentEngine.Store.Model {
    [XmlRoot("setting")]
    public class Setting: Entity {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        
        [XmlAttribute("value")]
        public string Value { get; set; } = string.Empty;
    }
}