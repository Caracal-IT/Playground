using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;

using ViewModel = Playground.PaymentEngine.Models.ApprovalRules;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("approval-rules")]
    public class ApprovalRuleController: ControllerBase {
        private readonly IMapper _mapper;
        
        public ApprovalRuleController(IMapper mapper) => 
            _mapper = mapper;

        [HttpPost("run")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleOutcome>>> RunApprovalRules([FromServices] RunApprovalRulesUseCase useCase, ViewModel.RunApprovalRuleRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleOutcome>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleOutcome>>(response.Outcomes));
            });

        [EnableQuery]
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>> GetApprovalRuleHistoryAsync([FromServices] GetApprovalRuleHistoriesUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>>(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleHistory>>(response.Histories));
            });

        [EnableQuery]
        [HttpGet("history/last-run")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>> GetLastRunApprovalRulesAsync([FromServices] GetLastRunApprovalRulesUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>>(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);;
                
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleHistory>>(response.Histories));
            });
    }
}