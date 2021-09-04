using System.Xml.Serialization;
using Playground.Core.Model;

namespace Playground.PaymentEngine.Application.UseCases.Shared {
    [XmlRoot("setting")]
    public record Setting: Entity {
        [XmlAttribute("name")]
        public string Name { get; set; } = string.Empty;
        
        [XmlAttribute("value")]
        public string Value { get; set; } = string.Empty;
    }
}