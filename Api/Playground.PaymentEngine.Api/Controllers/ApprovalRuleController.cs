namespace Playground.PaymentEngine.Api.Controllers;

using Models.ApprovalRules;
using Application.UseCases.ApprovalRules.GetApprovalRuleHistories;
using Application.UseCases.ApprovalRules.GetLastRunApprovalRules;
using Application.UseCases.ApprovalRules.RunApprovalRules;

using ApprovalRuleHistory = Models.ApprovalRules.ApprovalRuleHistory;
using ApprovalRuleOutcome = Models.ApprovalRules.ApprovalRuleOutcome;
using ViewModel = Models.ApprovalRules;

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