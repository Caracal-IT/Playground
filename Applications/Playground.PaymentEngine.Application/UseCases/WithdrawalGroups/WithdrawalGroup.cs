namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups;

using Playground.Core.Model;

public record WithdrawalGroup : Entity {
    public long CustomerId { get; set; }
    public List<long> WithdrawalIds { get; set; } = new();
}