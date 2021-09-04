using Playground.PaymentEngine.Api.Models.Shared;

namespace Playground.PaymentEngine.Api.Models.Customers {
    public record Customer {
        public long Id { get; set; }
        public decimal Balance { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}