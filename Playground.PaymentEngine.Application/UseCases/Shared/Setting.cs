using System.Xml.Serialization;

namespace Playground.PaymentEngine.Application.UseCases.Shared {
    [XmlRoot("setting")]
    public class Setting {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        
        [XmlAttribute("value")]
        public string Value { get; set; } = string.Empty;
    }
}