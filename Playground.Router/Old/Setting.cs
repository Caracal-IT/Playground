using System.Xml.Serialization;

namespace Playground.Router.Old {
    [XmlRoot("setting")]
    public class Setting {
        [XmlAttribute("name")] public string Name { get; set; } = "Setting";

        [XmlAttribute("value")] public string Value { get; set; } = string.Empty;

        public Setting() {}
        public Setting(string name, string value) {
            Name = name;
            Value = value;
        }
    }
}