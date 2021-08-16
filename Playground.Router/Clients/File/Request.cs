using System.Collections.Generic;
using System.Xml.Serialization;

namespace Playground.Router.Clients.File {
    [XmlRoot("request")]
    public class Request {
        [XmlElement("reference")] public string Reference { get; set; } = string.Empty;
        [XmlElement("code")] public string Code { get; set; } = string.Empty;
        [XmlElement("amount")] public decimal Amount { get; set; }
        [XmlElement("meta-data")] public List<Setting> MetaData { get; set; } = new();
    }
}