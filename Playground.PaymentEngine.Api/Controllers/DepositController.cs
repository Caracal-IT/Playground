using Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.DeleteDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposit;
using Playground.PaymentEngine.Application.UseCases.Deposits.GetDeposits;
using CreateDepositRequest = Playground.PaymentEngine.Application.UseCases.Deposits.CreateDeposit.CreateDepositRequest;
using Deposit = Playground.PaymentEngine.Api.Models.Deposits.Deposit;
using ViewModel = Playground.PaymentEngine.Api.Models.Deposits;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("deposits")]
    public class DepositController: ControllerBase {
        private readonly IMapper _mapper;
        
        public DepositController(IMapper mapper) => 
            _mapper = mapper;

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Deposit>>> GetAsync([FromServices] GetDepositsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<Deposit>>(response.Deposits));
            });
        
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Deposit>> GetAsync([FromServices] GetDepositUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<Deposit>>(async () => {
                var response = await useCase.ExecuteAsync(id, cancellationToken);
                
                if (response.Deposit == null)
                    return NotFound();
                
                return Ok(_mapper.Map<Deposit>(response.Deposit));
            });
        
        [HttpPost]
        public async Task<ActionResult<Deposit>> PostAsync([FromServices] CreateDepositUseCase useCase, [FromBody] Models.Deposits.CreateDepositRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(_mapper.Map<CreateDepositRequest>(request), cancellationToken);
                return Ok(_mapper.Map<Deposit>(result.Deposit));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> DeleteAsync([FromServices] DeleteDepositUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult>(async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
    }
}