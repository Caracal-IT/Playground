using Playground.Core.Model;
using Playground.PaymentEngine.Api.Models.Shared;

namespace Playground.PaymentEngine.Api.Models.Customers {
    public record Customer: Entity {
        public decimal Balance { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}