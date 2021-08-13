using System.Xml.Serialization;

namespace PaymentEngine.UseCases.Payments.Process {
    [XmlRoot("response")]
    public class ExportResponse {
        [XmlElement("reference")]
        public string Reference { get; set; }
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("code")]
        public string Code { get; set; }
        [XmlElement("message")]
        public string Message { get; set; }
        
        [XmlElement("terminal")]
        public string Terminal { get; set; }
    }
}