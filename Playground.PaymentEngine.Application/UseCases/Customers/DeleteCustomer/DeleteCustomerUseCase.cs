namespace Playground.PaymentEngine.Application.UseCases.Customers.DeleteCustomer {
    public class DeleteCustomerUseCase {
        private readonly CustomerStore _store;

        public DeleteCustomerUseCase(CustomerStore store) => _store = store;

        public async Task ExecuteAsync(long id, CancellationToken cancellationToken) => 
            await _store.DeleteCustomersAsync(new []{id}, cancellationToken);
    }
}