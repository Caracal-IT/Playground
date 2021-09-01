namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase {
        private readonly WithdrawalStore _store;
        private readonly IMapper _mapper;
        
        public GetWithdrawalsUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<GetWithdrawalsResponse> ExecuteAsync(CancellationToken cancellationToken) {
            var withdrawals = await _store.GetWithdrawalsAsync(cancellationToken);
            return new GetWithdrawalsResponse { Withdrawals = _mapper.Map<IEnumerable<Withdrawal>>(withdrawals)};
        }
    }
}