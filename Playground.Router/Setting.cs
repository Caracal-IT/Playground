using System.Xml.Serialization;

namespace Playground.Router {
    [XmlRoot("setting")]
    public class Setting {
        [XmlAttribute("name")] public string Name { get; set; } = "Setting";

        [XmlAttribute("value")] public string Value { get; set; } = string.Empty;
    }
}