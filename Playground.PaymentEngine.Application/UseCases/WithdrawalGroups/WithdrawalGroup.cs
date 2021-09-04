using Playground.Core.Model;

namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups {
    public record WithdrawalGroup: Entity {
        public long CustomerId { get; set; }
        public List<long> WithdrawalIds { get; set; } = new ();
    }
}