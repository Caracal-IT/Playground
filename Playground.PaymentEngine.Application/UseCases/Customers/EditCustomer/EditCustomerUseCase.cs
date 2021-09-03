using System.Linq;
using Data = Playground.PaymentEngine.Store.Model;

namespace Playground.PaymentEngine.Application.UseCases.Customers.EditCustomer {
    public class EditCustomerUseCase {
        private readonly CustomerStore _store;
        private readonly IMapper _mapper;
        
        public EditCustomerUseCase(CustomerStore store, IMapper mapper) {
            _store = store;
            _mapper = mapper;
        }
        
        public async Task<EditCustomerResponse> ExecuteAsync(EditCustomerRequest request, CancellationToken cancellationToken) {
            var customers = await _store.GetCustomersAsync(new []{request.CustomerId}, cancellationToken);
            var customer = customers.FirstOrDefault();
            
            if(customer == null)
                return new EditCustomerResponse();

            if (request.Balance != null)
                customer.Balance = request.Balance.Value;

            var deleted = request.MetaData.Where(d => d.Remove).Select(m => m.Name);
            var modified = request.MetaData.Where(d => !d.Remove);

            customer.MetaData = customer.MetaData.Where(i => !deleted.Contains(i.Name)).ToList();

            customer.MetaData
                    .Join(modified, m => m.Name, u => u.Name, (m, u) => new {MetaData = m, UpdateMetaData = u})
                    .ToList()
                    .ForEach(i => i.MetaData.Value = i.UpdateMetaData.Value);

            var items = customer.MetaData.Select(m => m.Name);
            var newItems = request.MetaData
                                  .Where(m => !items.Contains(m.Name))
                                  .Select(m => _mapper.Map<Data.MetaData>(m));
            
            customer.MetaData.AddRange(newItems);

            await _store.UpdateCustomerAsync(customer, cancellationToken);
            
            //  return new GetCustomerResponse { Customer = _mapper.Map<Customer>(customers.FirstOrDefault()) };
            return new EditCustomerResponse {
                Customer = _mapper.Map<Customer>(customer)
            };
        }
    }
}