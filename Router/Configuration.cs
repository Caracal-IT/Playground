using System.Collections.Generic;
using System.Xml.Serialization;

namespace Router {
    [XmlRoot("config")]
    public class Configuration {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public List<Setting> Settings { get; set; } = new();
    }

    [XmlRoot("setting")]
    public class Setting {
        [XmlAttribute("name")]
        public string Name { get; set; }
        
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}