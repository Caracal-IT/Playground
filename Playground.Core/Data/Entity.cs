using System.Xml.Serialization;

namespace Playground.Core.Data {
    public class Entity {
        [XmlAttribute("id")]
        public long Id { get; set; }
    }
}