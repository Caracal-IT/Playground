using System.Linq;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.UseCases.ApprovalRules.RunApprovalRules;

using ViewModel = Playground.PaymentEngine.Models.ApprovalRules;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("approval-rule")]
    public class ApprovalRuleController: ControllerBase {
        private readonly IMapper _mapper;
        private readonly ApprovalRuleStore _approvalRuleStore;
        
        public ApprovalRuleController(IMapper mapper, ApprovalRuleStore approvalRuleStore) {
            _mapper = mapper;
            _approvalRuleStore = approvalRuleStore;
        }
        
        [HttpPost("run")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleOutcome>>> RunApprovalRules([FromServices] RunApprovalRulesUseCase useCase, ViewModel.RunApprovalRuleRequest request, CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleOutcome>>>(async () => {
                var response = await useCase.ExecuteAsync(request.WithdrawalGroups, cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleOutcome>>(response.Outcomes));
            });

        [EnableQuery]
        [HttpGet("history")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>> GetApprovalRuleHistoryAsync(CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>>(async () => {
                var response = await _approvalRuleStore.GetRuleHistoriesAsync(cancellationToken);
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleHistory>>(response));
            });

        [EnableQuery]
        [HttpGet("history/last-run")]
        public async Task<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>> GetLatestApprovalRulesAsync(CancellationToken cancellationToken) =>
            await ExecuteAsync<ActionResult<IEnumerable<ViewModel.ApprovalRuleHistory>>>(async () => {
                var response = await _approvalRuleStore.GetRuleHistoriesAsync(cancellationToken);
                
                return Ok(_mapper.Map<IEnumerable<ViewModel.ApprovalRuleHistory>>(response.GroupBy(r => r.WithdrawalGroupId, (_, h) => h.Last())));
            });
    }
}