using System.Xml.Serialization;
using Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Application.UseCases.Payments.Process {
    [XmlRoot("response")]
    public record ExportResponse {
        [XmlElement("reference")]
        public string? Reference { get; set; }
        [XmlElement("name")]
        public string? Name { get; set; }
        [XmlElement("code")]
        public string? Code { get; set; }
        [XmlElement("message")]
        public string? Message { get; set; }
        
        [XmlElement("terminal")]
        public string? Terminal { get; set; }
        
        [XmlArray("meta-data")]
        [XmlArrayItem("meta-data-item")]
        public List<MetaData> MetaData { get; set; } = new();
    }
}