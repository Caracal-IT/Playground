namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer;

public class GetCustomerUseCase {
    private readonly CustomerStore _store;
    private readonly IMapper _mapper;
    
    public GetCustomerUseCase(CustomerStore store, IMapper mapper) {
        _store = store;
        _mapper = mapper;
    }

    public async Task<GetCustomerResponse> ExecuteAsync(long id, CancellationToken cancellationToken) {
        var customers = await _store.GetCustomersAsync(new []{id}, cancellationToken);
        return new GetCustomerResponse { Customer = _mapper.Map<Customer>(customers.FirstOrDefault()) };
    }
}