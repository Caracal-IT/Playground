namespace Playground.Router;

[XmlRoot("config")]
public class Configuration {
    [XmlAttribute("name")] public string? Name { get; set; }

    [XmlArray("settings")]
    [XmlArrayItem("setting")]
    public List<Setting> Settings { get; set; } = new();
}