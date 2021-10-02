namespace Playground.PaymentEngine.Application.UseCases.Deposits;

using Core.Model;

public record Deposit : Entity {
    public long AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDate { get; set; }
    public List<MetaData> MetaData { get; set; } = new();
}