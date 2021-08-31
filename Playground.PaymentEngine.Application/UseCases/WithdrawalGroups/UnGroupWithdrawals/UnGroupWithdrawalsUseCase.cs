namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.UnGroupWithdrawals {
    public class UnGroupWithdrawalsUseCase {
        private readonly WithdrawalStore _store;

        public UnGroupWithdrawalsUseCase(WithdrawalStore store) => 
            _store = store;

        public async Task ExecuteAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            await _store.UnGroupWithdrawalsAsync(withdrawalGroupId, cancellationToken);
        }
    }
}