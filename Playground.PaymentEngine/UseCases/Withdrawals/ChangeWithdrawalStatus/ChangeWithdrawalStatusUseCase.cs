using Playground.PaymentEngine.Store.Withdrawals;

namespace Playground.PaymentEngine.UseCases.Withdrawals.ChangeWithdrawalStatus {
    public class ChangeWithdrawalStatusUseCase {
        private readonly WithdrawalStore _store;    
        
        public ChangeWithdrawalStatusUseCase(WithdrawalStore store) => 
            _store = store;

        public async Task ExecuteAsync(long id, long statusId, CancellationToken cancellationToken) =>
            await _store.UpdateWithdrawalStatusAsync(new []{id}, statusId, cancellationToken);
    }
}