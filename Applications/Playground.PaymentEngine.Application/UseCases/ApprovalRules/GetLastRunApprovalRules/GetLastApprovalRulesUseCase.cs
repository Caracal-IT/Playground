namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.GetLastRunApprovalRules {
    public class GetLastRunApprovalRulesUseCase {
        private readonly ApprovalRuleStore _approvalRuleStore;
        private readonly IMapper _mapper;
        
        public GetLastRunApprovalRulesUseCase(ApprovalRuleStore approvalRuleStore, IMapper mapper) {
            _approvalRuleStore = approvalRuleStore;
            _mapper = mapper;
        }

        public async Task<GetGetLastRunApprovalRulesResponse> ExecuteAsync(CancellationToken cancellationToken) {
            var histories = await _approvalRuleStore.GetLastRunApprovalRulesAsync(cancellationToken);
      
            return new GetGetLastRunApprovalRulesResponse {
                Histories = _mapper.Map<IEnumerable<ApprovalRuleHistory>>(histories)
            };
        }
    }
}