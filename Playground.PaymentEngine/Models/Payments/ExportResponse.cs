using Playground.PaymentEngine.Stores.Model;

namespace Playground.PaymentEngine.Models.Payments {
    public class ExportResponse {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Terminal { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}