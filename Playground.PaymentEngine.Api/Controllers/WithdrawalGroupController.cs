using Playground.PaymentEngine.Api.Models.Withdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.AppendGroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals;
using Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.UnGroupWithdrawals;
using ViewModel = Playground.PaymentEngine.Api.Models.Withdrawals;
using WithdrawalGroup = Playground.PaymentEngine.Api.Models.Withdrawals.WithdrawalGroup;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("withdrawals/groups")]
    public class WithdrawalGroupController: ControllerBase {
        private readonly IMapper _mapper;
        
        public WithdrawalGroupController(IMapper mapper) => 
            _mapper = mapper;
        
        [HttpPost]
        public Task<ActionResult<IEnumerable<WithdrawalGroup>>> Post([FromServices] GroupWithdrawalsUseCase useCase, [FromBody] GroupWithdrawalRequest request, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult<IEnumerable<WithdrawalGroup>>> (async () => {
                var response = await useCase.ExecuteAsync(request.Withdrawals, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<WithdrawalGroup>>(response.WithdrawalGroups));
            });
        
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<WithdrawalGroup>>> GetAsync([FromServices] GetWithdrawalGroupsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(new GetWithdrawalGroupsRequest(), cancellationToken);
                return Ok(_mapper.Map<IEnumerable<WithdrawalGroup>>(result.WithdrawalGroups));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> Delete([FromServices] UnGroupWithdrawalsUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult> (async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
        
        [HttpPatch("{id:long}")]
        public Task<ActionResult<WithdrawalGroup>> Patch([FromServices] AppendGroupWithdrawalsUseCase useCase, [FromRoute] long id, [FromBody] GroupWithdrawalRequest request, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult<WithdrawalGroup>> (async () => {
                var result = await useCase.ExecuteAsync(id, request.Withdrawals, cancellationToken);
                return Ok(_mapper.Map<WithdrawalGroup>(result.WithdrawalGroup));
            });
    }
}