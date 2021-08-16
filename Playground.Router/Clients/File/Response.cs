using System;
using System.Xml.Serialization;

namespace Playground.Router.Clients.File {
    [XmlRoot("response")]
    public class Response {
        [XmlAttribute("reference")] public string Reference { get; set; } = string.Empty;
        [XmlElement("name")] public string Name { get; set; } = string.Empty;
        [XmlElement("code")] public string Code { get; set; } = string.Empty;
        [XmlElement("amount")] public decimal Amount { get; set; }
        [XmlElement("file-name")] public string FileName { get; set; } = string.Empty;
    }
}