using System.Linq;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.UseCases.Payments.RunApprovalRules;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("approval-rule")]
    public class ApprovalRuleController {
        private readonly IMapper _mapper;
        private readonly ApprovalRuleStore _approvalRuleStore;
        
        public ApprovalRuleController(IMapper mapper, ApprovalRuleStore approvalRuleStore) {
            _mapper = mapper;
            _approvalRuleStore = approvalRuleStore;
        }
        
        [HttpPost("run")]
        public Task<RunApprovalRulesResponse> RunApprovalRules([FromServices] RunApprovalRulesUseCase useCase, RunApprovalRulesRequest request, CancellationToken cancellationToken) => 
            useCase.ExecuteAsync(request, cancellationToken);

        [HttpPost()]
        public Task<IEnumerable<ApprovalRuleHistory>> GetApprovalRulesAsync(List<long> request, CancellationToken cancellationToken) =>
            _approvalRuleStore.GetRuleHistoriesAsync(request, cancellationToken);
        
        [HttpPost("last")]
        public async Task<IEnumerable<ApprovalRuleHistory>> GetLatestApprovalRulesAsync(List<long> request, CancellationToken cancellationToken) {
            var rules = await _approvalRuleStore.GetRuleHistoriesAsync(request, cancellationToken);
            return rules.GroupBy(r => r.WithdrawalGroupId, (_, h) => h.Last());
        }
    }
}