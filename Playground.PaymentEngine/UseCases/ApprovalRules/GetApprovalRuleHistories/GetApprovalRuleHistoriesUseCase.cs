using Playground.PaymentEngine.Stores.ApprovalRules;

namespace Playground.PaymentEngine.UseCases.ApprovalRules.GetApprovalRuleHistories {
    public class GetApprovalRuleHistoriesUseCase {
        private readonly ApprovalRuleStore _approvalRuleStore;
        private readonly IMapper _mapper;
        
        public GetApprovalRuleHistoriesUseCase(ApprovalRuleStore approvalRuleStore, IMapper mapper) {
            _approvalRuleStore = approvalRuleStore;
            _mapper = mapper;
        }

        public async Task<GetApprovalRuleHistoriesResponse> ExecuteAsync(CancellationToken cancellationToken) {
            var histories = await _approvalRuleStore.GetRuleHistoriesAsync(cancellationToken);
            return new GetApprovalRuleHistoriesResponse {
                Histories = _mapper.Map<IEnumerable<ApprovalRuleHistory>>(histories)
            };
        }
    }
}