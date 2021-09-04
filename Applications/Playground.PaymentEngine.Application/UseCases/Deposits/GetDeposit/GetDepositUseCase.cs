using System.Linq;
using Playground.PaymentEngine.Store.Deposits;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit {
    public class GetDepositUseCase {
        private readonly DepositStore _store;
        private readonly IMapper _mapper;
        
        public GetDepositUseCase(DepositStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }

        public async Task<GetDepositResponse> ExecuteAsync(long id, CancellationToken cancellationToken) {
            var deposits = await _store.GetDepositsAsync(new []{id}, cancellationToken);
            return new GetDepositResponse { Deposit = _mapper.Map<Deposit>(deposits.FirstOrDefault()) };
        }
    }
}