namespace Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;

public class GetCustomersUseCase {
    private readonly CustomerStore _store;
    private readonly IMapper _mapper;
    
    public GetCustomersUseCase(CustomerStore store, IMapper mapper) {
        _store = store;
        _mapper = mapper;
    }

    public async Task<GetCustomersResponse> ExecuteAsync(CancellationToken cancellationToken) {
        var customers = await _store.GetCustomersAsync(cancellationToken);
        return new GetCustomersResponse { Customers = _mapper.Map<IEnumerable<Customer>>(customers) };
    }
}