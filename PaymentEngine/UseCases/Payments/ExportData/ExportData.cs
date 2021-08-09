using System.Collections.Generic;
using System.Xml.Serialization;
using PaymentEngine.Model;

namespace PaymentEngine.UseCases.Payments.ExportData {
    [XmlRoot(ElementName = "export-data")]
    public class ExportData {
        [XmlIgnore] public List<ExportAllocation> Allocations { get; set; } = new();
        [XmlIgnore] public List<ExportResponseData> Response { get; set; } = new();
        
        [XmlAttribute(AttributeName="reference")]
        public string Reference { get; set; }

        [XmlAttribute(AttributeName="amount", DataType="decimal")]
        public decimal Amount { get; set; }
        
        [XmlAttribute(AttributeName="account-type-id")]
        public long AccountTypeId { get; set; }

        [XmlElement(ElementName="meta-data")]
        public List<MetaData> MetaData { get; set; } = new();
    }
}