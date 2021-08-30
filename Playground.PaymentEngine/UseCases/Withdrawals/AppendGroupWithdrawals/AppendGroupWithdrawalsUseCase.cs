using Playground.PaymentEngine.Store.Withdrawals;

namespace Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals {
    public class AppendGroupWithdrawalsUseCase {
        private readonly WithdrawalStore _store;  
        private readonly IMapper _mapper;
        
        public AppendGroupWithdrawalsUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }

        public async Task<GroupWithdrawalsResponse> ExecuteAsync(long groupId, IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
            var groups = await _store.AppendWithdrawalGroupsAsync(groupId, withdrawalIds, cancellationToken);
            return new GroupWithdrawalsResponse {
                WithdrawalGroup = _mapper.Map<WithdrawalGroup>(groups)
            };
        }
    }
}