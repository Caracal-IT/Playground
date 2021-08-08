using System.Collections.Generic;
using System.Xml.Serialization;
using PaymentEngine.Model;

namespace PaymentEngine.UseCases.Payments.Process {
    [XmlRoot(ElementName="export-data")]
    public class ExportData {
        [XmlIgnore] public List<long> Allocations { get; set; } = new List<long>();
        
        [XmlAttribute(AttributeName="reference")]
        public string Reference { get; set; }
        
        [XmlAttribute(AttributeName="method")]
        public string Method { get; set; }
        
        [XmlAttribute(AttributeName="account-type-id")]
        public long AccountTypeId { get; set; }
        
        [XmlAttribute(AttributeName="amount", DataType="decimal")]
        public decimal Amount { get; set; }
        
        [XmlElement(ElementName="meta-data")]
        public List<MetaData> MetaData { get; set; } = new();
    }
}