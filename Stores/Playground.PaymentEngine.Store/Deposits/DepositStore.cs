namespace Playground.PaymentEngine.Store.Deposits;

using Model;

public interface DepositStore {
    Task<IEnumerable<Deposit>> GetDepositsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<Deposit>> GetDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken);
    Task<Deposit> CreateDepositAsync(Deposit deposit, CancellationToken cancellationToken);
    Task DeleteDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken);
}