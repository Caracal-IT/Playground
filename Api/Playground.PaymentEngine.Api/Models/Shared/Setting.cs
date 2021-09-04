using Playground.Core.Model;

namespace Playground.PaymentEngine.Api.Models.Shared {
    public record Setting: Entity {
        public string Name { get; init; }
        public string Value { get; init; }
    }
}