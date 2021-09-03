namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups {
    public record WithdrawalGroup(long Id, long CustomerId, List<long> WithdrawalIds);
}