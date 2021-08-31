using System.Linq;

namespace Playground.PaymentEngine.Application.UseCases.Withdrawals.GetWithdrawal {
    public class GetWithdrawalUseCase {
        private readonly WithdrawalStore _store;
        private readonly IMapper _mapper;
        
        public GetWithdrawalUseCase(WithdrawalStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<GetWithdrawalResponse> ExecuteAsync(long id, CancellationToken cancellationToken) {
            var withdrawals = await _store.GetWithdrawalsAsync(new []{id}, cancellationToken);
            var withdrawal = withdrawals.FirstOrDefault();
            
            return new GetWithdrawalResponse { Withdrawal = withdrawal == null ? null : _mapper.Map<Withdrawal>(withdrawal)};
        }
    }
}