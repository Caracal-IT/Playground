namespace Playground.PaymentEngine.Api.Controllers;

using Models.Withdrawals;
using Application.UseCases.Withdrawals.ChangeWithdrawalStatus;
using Application.UseCases.Withdrawals.CreateWithdrawal;
using Application.UseCases.Withdrawals.DeleteWithdrawal;
using Application.UseCases.Withdrawals.GetWithdrawal;
using Application.UseCases.Withdrawals.GetWithdrawals;

using CreateWithdrawalRequest = Application.UseCases.Withdrawals.CreateWithdrawal.CreateWithdrawalRequest;
using ViewModel = Models.Withdrawals;
using Withdrawal = Models.Withdrawals.Withdrawal;

[ApiController]
[Route("withdrawals")]
public class WithdrawalController: ControllerBase {
    private readonly IMapper _mapper;
    
    public WithdrawalController(IMapper mapper) => 
        _mapper = mapper;
    
    [HttpGet]
    [EnableQuery]
    public async Task<ActionResult<IEnumerable<Withdrawal>>> GetAsync([FromServices] GetWithdrawalsUseCase useCase, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var result = await useCase.ExecuteAsync(cancellationToken);
            return Ok(_mapper.ProjectTo<Withdrawal>(result.Withdrawals));
        });
    
    [HttpPost]
    public async Task<ActionResult<Withdrawal>> PostAsync([FromServices] CreateWithdrawalUseCase useCase, [FromBody] Models.Withdrawals.CreateWithdrawalRequest request, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var result = await useCase.ExecuteAsync(_mapper.Map<CreateWithdrawalRequest>(request), cancellationToken);
            return Ok(_mapper.Map<Withdrawal>(result.Withdrawal));
        });

    [HttpGet("{id:long}")]
    public Task<ActionResult<Withdrawal>> GetAsync([FromServices] GetWithdrawalUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult<Withdrawal>>(async () => {
            var response = await useCase.ExecuteAsync(id, cancellationToken);

            if (response?.Withdrawal == null)
                return NotFound();
            
            return Ok(_mapper.Map<Withdrawal>(response.Withdrawal));
        });
    
    [HttpDelete("{id:long}")]
    public Task<ActionResult> DeleteAsync([FromServices] DeleteWithdrawalUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult>(async () => {
            await useCase.ExecuteAsync(id, cancellationToken);
            return NoContent();
        });
    
    [HttpPatch("{id:long}")]
    public Task<ActionResult> UpdateStatusAsync([FromServices] ChangeWithdrawalStatusUseCase useCase, [FromRoute] long id, [FromBody] Status status, CancellationToken cancellationToken) =>
        ExecuteAsync<ActionResult>(async () => {
            await useCase.ExecuteAsync(id, status.StatusId, cancellationToken);
            return NoContent();
        });
}