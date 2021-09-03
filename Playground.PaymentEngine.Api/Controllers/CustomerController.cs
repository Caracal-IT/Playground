using Playground.PaymentEngine.Application.UseCases.Customers.CreateCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.DeleteCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.GetCustomer;
using Playground.PaymentEngine.Application.UseCases.Customers.GetCustomers;
using Customer = Playground.PaymentEngine.Api.Models.Customers.Customer;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("customers")]
    public class CustomerController: ControllerBase {
        private readonly IMapper _mapper;
        
        public CustomerController(IMapper mapper) => 
            _mapper = mapper;
        
        [HttpPost]
        public async Task<ActionResult<Customer>> PostAsync([FromServices] CreateCustomerUseCase useCase, [FromBody] Models.Customers.CreateCustomerRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(_mapper.Map<CreateCustomerRequest>(request), cancellationToken);
                return Ok(_mapper.Map<Customer>(result.Customer));
            });
        
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAsync([FromServices] GetCustomersUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<Customer>>(response.Customers));
            });
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Customer>> GetAsync([FromServices] GetCustomerUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var response = await useCase.ExecuteAsync(id, cancellationToken);
                return Ok(_mapper.Map<Customer>(response.Customer));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> DeleteAsync([FromServices] DeleteCustomerUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult>(async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
    }
}