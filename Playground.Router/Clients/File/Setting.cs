using System.Xml.Serialization;

namespace Playground.Router.Clients.File {
    public class Setting {
        [XmlAttribute("name")] public string Name { get; set; } = string.Empty;
        [XmlAttribute("value")] public string Value { get; set; } = string.Empty;
        
        public Setting() {}

        public Setting(string name, string value) {
            Name = name;
            Value = value;
        }
    }
}