namespace Playground.PaymentEngine.Api.Models.Deposits;

public record CreateDepositRequest(long AccountId, decimal Amount, DateTime DepositDate) {
    public List<MetaData> MetaData { get; set; } = new();
}