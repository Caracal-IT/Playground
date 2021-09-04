using Playground.Core.Model;
using Playground.PaymentEngine.Application.UseCases.Shared;

namespace Playground.PaymentEngine.Application.UseCases.Customers {
    public record Customer: Entity {
        public decimal Balance { get; set; }
        public List<MetaData> MetaData { get; set; } = new();
    }
}