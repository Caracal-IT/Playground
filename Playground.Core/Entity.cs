using System.Xml.Serialization;

namespace Playground.Core {
    public class Entity {
        [XmlAttribute("id")]
        public long Id { get; set; }
    }
}