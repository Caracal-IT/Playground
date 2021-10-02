namespace Playground.PaymentEngine.Api.Controllers;

using Application.UseCases.Customers.CreateCustomer;
using Application.UseCases.Customers.DeleteCustomer;
using Application.UseCases.Customers.EditCustomer;
using Application.UseCases.Customers.GetCustomer;
using Application.UseCases.Customers.GetCustomers;

using ViewModel = Models.Customers;

[ApiController]
[Route("customers")]
public class CustomerController: ControllerBase {
    private readonly IMapper _mapper;
    
    public CustomerController(IMapper mapper) => 
        _mapper = mapper;
    
    [HttpPost]
    public async Task<ActionResult<ViewModel.Customer>> PostAsync([FromServices] CreateCustomerUseCase useCase, [FromBody] ViewModel.CreateCustomerRequest request, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var result = await useCase.ExecuteAsync(_mapper.Map<CreateCustomerRequest>(request), cancellationToken);
            return Ok(_mapper.Map<ViewModel.Customer>(result.Customer));
        });
    
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<ViewModel.Customer>>> GetAsync([FromServices] GetCustomersUseCase useCase, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var response = await useCase.ExecuteAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ViewModel.Customer>>(response.Customers));
        });
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ViewModel.Customer>> GetAsync([FromServices] GetCustomerUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var response = await useCase.ExecuteAsync(id, cancellationToken);
            return Ok(_mapper.Map<ViewModel.Customer>(response.Customer));
        });
    
    [HttpDelete("{id:long}")]
    public Task<ActionResult> DeleteAsync([FromServices] DeleteCustomerUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult>(async () => {
            await useCase.ExecuteAsync(id, cancellationToken);
            return NoContent();
        });
    
    [HttpPatch("{id:long}")]
    public Task<ActionResult<ViewModel.Customer>> PatchAsync([FromServices] EditCustomerUseCase useCase, [FromRoute] long id, [FromBody] ViewModel.EditCustomerRequest request, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult<ViewModel.Customer>>(async () => {
            var req = _mapper.Map<EditCustomerRequest>(request);
            req.CustomerId = id;
            
            var response = await useCase.ExecuteAsync(req, cancellationToken);
            return Ok(_mapper.Map<ViewModel.Customer>(response.Customer));
        });
}