namespace Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits {
    public class GetDepositsUseCase {
        private readonly DepositStore _store;
        private readonly IMapper _mapper;
        
        public GetDepositsUseCase(DepositStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }

        public async Task<GetDepositsResponse> ExecuteAsync(CancellationToken cancellationToken) {
            var deposits = await _store.GetDepositsAsync(cancellationToken);
            return new GetDepositsResponse { Deposits = _mapper.Map<IEnumerable<Deposit>>(deposits) };
        }
    }
}