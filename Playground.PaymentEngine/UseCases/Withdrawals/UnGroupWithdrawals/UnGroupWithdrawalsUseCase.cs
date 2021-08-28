using Playground.PaymentEngine.Stores.Withdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals;

namespace Playground.PaymentEngine.UseCases.Withdrawals.UnGroupWithdrawals {
    public class UnGroupWithdrawalsUseCase {
        private readonly WithdrawalStore _store;

        public UnGroupWithdrawalsUseCase(WithdrawalStore store) => 
            _store = store;

        public async Task ExecuteAsync(long withdrawalGroupId, CancellationToken cancellationToken) {
            await _store.UnGroupWithdrawalsAsync(withdrawalGroupId, cancellationToken);
        }
    }
}