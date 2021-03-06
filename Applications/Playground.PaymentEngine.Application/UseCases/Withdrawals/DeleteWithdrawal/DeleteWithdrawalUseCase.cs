namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.DeleteWithdrawal;

public class DeleteWithdrawalUseCase {
    private readonly WithdrawalStore _store;

    public DeleteWithdrawalUseCase(WithdrawalStore store) =>
        _store = store;

    public async Task ExecuteAsync(long id, CancellationToken cancellationToken) =>
        await _store.DeleteWithdrawalsAsync(new[] { id }, cancellationToken);
}