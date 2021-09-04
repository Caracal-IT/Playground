using Playground.PaymentEngine.Api.Models.Shared;

namespace Playground.PaymentEngine.Api.Models.Payments {
    public record ExportResponse {
        public string Reference { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Terminal { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}