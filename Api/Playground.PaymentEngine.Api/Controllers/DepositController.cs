namespace Playground.PaymentEngine.Api.Controllers;

using Application.UseCases.Deposits.CreateDeposit;
using Application.UseCases.Deposits.DeleteDeposit;
using Application.UseCases.Deposits.GetDeposit;
using Application.UseCases.Deposits.GetDeposits;

using CreateDepositRequest = Application.UseCases.Deposits.CreateDeposit.CreateDepositRequest;
using ViewModel = Models.Deposits;

[ApiController]
[Route("deposits")]
public class DepositController: ControllerBase {
    private readonly IMapper _mapper;
    
    public DepositController(IMapper mapper) => 
        _mapper = mapper;

    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<ViewModel.Deposit>>> GetAsync([FromServices] GetDepositsUseCase useCase, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var response = await useCase.ExecuteAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ViewModel.Deposit>>(response.Deposits));
        });
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<ViewModel.Deposit>> GetAsync([FromServices] GetDepositUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        await ExecuteAsync<ActionResult<ViewModel.Deposit>>(async () => {
            var response = await useCase.ExecuteAsync(id, cancellationToken);
            
            if (response.Deposit == null)
                return NotFound();
            
            return Ok(_mapper.Map<ViewModel.Deposit>(response.Deposit));
        });
    
    [HttpPost]
    public async Task<ActionResult<ViewModel.Deposit>> PostAsync([FromServices] CreateDepositUseCase useCase, [FromBody] ViewModel.CreateDepositRequest request, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var result = await useCase.ExecuteAsync(_mapper.Map<CreateDepositRequest>(request), cancellationToken);
            return Ok(_mapper.Map<ViewModel.Deposit>(result.Deposit));
        });
    
    [HttpDelete("{id:long}")]
    public Task<ActionResult> DeleteAsync([FromServices] DeleteDepositUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult>(async () => {
            await useCase.ExecuteAsync(id, cancellationToken);
            return NoContent();
        });
}