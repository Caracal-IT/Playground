using Playground.PaymentEngine.Api.Models.ApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
using ApprovalRuleHistory = Playground.PaymentEngine.Api.Models.ApprovalRules.ApprovalRuleHistory;
using ApprovalRuleOutcome = Playground.PaymentEngine.Api.Models.ApprovalRules.ApprovalRuleOutcome;
using ViewModel = Playground.PaymentEngine.Api.Models.ApprovalRules;

namespace Playground.PaymentEngine.Api.Controllers {
    [ApiController]
    [Route("approval-rules")]
    public class ApprovalRuleController: ControllerBase {
        private readonly IMapper _mapper;
        
        public ApprovalRuleController(IMapper mapper) => 
            _mapper = mapper;

        [HttpPost("run")]
        public async Task<ActionResult<IEnumerable<ApprovalRuleOutcome>>> RunApprovalRules([FromServices] RunApprovalRulesUseCase useCase, RunApprovalRuleRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ApprovalRuleOutcome>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ApprovalRuleOutcome>>(response.Outcomes));
            });

        [EnableQuery]
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<ApprovalRuleHistory>>> GetApprovalRuleHistoryAsync([FromServices] GetApprovalRuleHistoriesUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ApprovalRuleHistory>>>(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ApprovalRuleHistory>>(response.Histories));
            });

        [EnableQuery]
        [HttpGet("history/last-run")]
        public async Task<ActionResult<IEnumerable<ApprovalRuleHistory>>> GetLastRunApprovalRulesAsync([FromServices] GetLastRunApprovalRulesUseCase useCase, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ApprovalRuleHistory>>>(async () => {
                var response = await useCase.ExecuteAsync(cancellationToken);;
                
                return Ok(_mapper.Map<IEnumerable<ApprovalRuleHistory>>(response.Histories));
            });
    }
}