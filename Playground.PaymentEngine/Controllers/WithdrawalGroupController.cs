using Playground.PaymentEngine.UseCases.WithdrawalGroups.GetWithdrawalGroups;
using Playground.PaymentEngine.UseCases.Withdrawals.AppendGroupWithdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals;
using Playground.PaymentEngine.UseCases.Withdrawals.UnGroupWithdrawals;
using ViewModel = Playground.PaymentEngine.Models.Withdrawals;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("withdrawal/group")]
    public class WithdrawalGroupController: ControllerBase {
        private readonly IMapper _mapper;
        
        public WithdrawalGroupController(IMapper mapper) => 
            _mapper = mapper;
        
        [HttpPost()]
        public Task<ActionResult<IEnumerable<ViewModel.WithdrawalGroup>>> Post([FromServices] GroupWithdrawalsUseCase useCase, [FromBody] ViewModel.GroupWithdrawalRequest request, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult<IEnumerable<ViewModel.WithdrawalGroup>>> (async () => {
                var response = await useCase.ExecuteAsync(request.Withdrawals, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.WithdrawalGroup>>(response.WithdrawalGroups));
            });
        
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<ViewModel.WithdrawalGroup>>> GetAsync([FromServices] GetWithdrawalGroupsUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync(async () => {
                var result = await useCase.ExecuteAsync(new GetWithdrawalGroupsRequest(), cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.WithdrawalGroup>>(result.WithdrawalGroups));
            });
        
        [HttpDelete("{id:long}")]
        public Task<ActionResult> Delete([FromServices] UnGroupWithdrawalsUseCase useCase, [FromRoute] long id, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult> (async () => {
                await useCase.ExecuteAsync(id, cancellationToken);
                return NoContent();
            });
        
        [HttpPatch("{id:long}")]
        public Task<ActionResult<ViewModel.WithdrawalGroup>> Patch([FromServices] AppendGroupWithdrawalsUseCase useCase, [FromRoute] long id, [FromBody] ViewModel.GroupWithdrawalRequest request, CancellationToken cancellationToken) =>
            ExecuteAsync<ActionResult<ViewModel.WithdrawalGroup>> (async () => {
                var result = await useCase.ExecuteAsync(id, request.Withdrawals, cancellationToken);
                return Ok(_mapper.Map<ViewModel.WithdrawalGroup>(result.WithdrawalGroup));
            });
    }
}