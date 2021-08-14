using System.Xml.Serialization;

namespace Router {
    [XmlRoot("setting")]
    public class Setting {
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}