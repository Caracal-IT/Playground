using Data = Playground.PaymentEngine.Store.Customers.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer {
    public class CreateCustomerUseCase {
        private readonly CustomerStore _store;
        private readonly IMapper _mapper;
        
        public CreateCustomerUseCase(CustomerStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<CreateCustomerResponse> ExecuteAsync(CreateCustomerRequest request, CancellationToken cancellationToken) {
            var deposit = _mapper.Map<Data.Customer>(request);
            var response = await  _store.CreateCustomerAsync(deposit, cancellationToken);
            
            return new CreateCustomerResponse{ Customer = _mapper.Map<Customer>(response) };
        }
    }
}