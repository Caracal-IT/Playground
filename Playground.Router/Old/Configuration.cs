using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.Router.Old {
    [XmlRoot("config")]
    public class Configuration {
        [XmlAttribute("name")]
        public string? Name { get; set; }

        [XmlArray("settings")]
        [XmlArrayItem("setting")]
        public List<Setting> Settings { get; set; } = new();
    }
}