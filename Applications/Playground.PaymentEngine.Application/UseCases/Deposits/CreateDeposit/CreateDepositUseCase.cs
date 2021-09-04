using Data = Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit {
    public class CreateDepositUseCase {
        private readonly DepositStore _store;
        private readonly IMapper _mapper;
        
        public CreateDepositUseCase(DepositStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<CreateDepositResponse> ExecuteAsync(CreateDepositRequest request, CancellationToken cancellationToken) {
            var deposit = _mapper.Map<Data.Deposit>(request);
            var response = await  _store.CreateDepositAsync(deposit, cancellationToken);
            
            return new CreateDepositResponse{ Deposit = _mapper.Map<Deposit>(response) };
        }
    }
}