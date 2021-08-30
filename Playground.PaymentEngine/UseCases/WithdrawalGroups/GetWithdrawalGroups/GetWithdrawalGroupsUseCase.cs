using Playground.PaymentEngine.Store.Withdrawals;

namespace Playground.PaymentEngine.UseCases.WithdrawalGroups.GetWithdrawalGroups {
    public class GetWithdrawalGroupsUseCase {
        private readonly WithdrawalStore _store;  
        private readonly IMapper _mapper;
        
        public GetWithdrawalGroupsUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }

        public async Task<GetWithdrawalGroupsResponse> ExecuteAsync(GetWithdrawalGroupsRequest request, CancellationToken cancellationToken) {
            var groups = await _store.GetWithdrawalGroupsAsync(cancellationToken);
            return new GetWithdrawalGroupsResponse {
                WithdrawalGroups = _mapper.Map<IEnumerable<WithdrawalGroup>>(groups)
            };
        }
    }
}