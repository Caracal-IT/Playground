namespace Playground.PaymentEngine.Application.UseCases.Deposits.DeleteDeposit;

public class DeleteDepositUseCase {
    private readonly DepositStore _store;

    public DeleteDepositUseCase(DepositStore store) => _store = store;

    public async Task ExecuteAsync(long id, CancellationToken cancellationToken) =>
        await _store.DeleteDepositsAsync(new[] { id }, cancellationToken);
}