namespace Playground.PaymentEngine.Application.UseCases.ApprovalRules.RunApprovalRules;
    
using Core.Extensions;
using Playground.PaymentEngine.Store.Withdrawals.Model;
using Playground.Rules;
using ActionResult = RulesEngine.Models.ActionResult;

using Data = Playground.PaymentEngine.Store.ApprovalRules.Model;
using SharedData = Store.Model;

public class RunApprovalRulesUseCase {
    private const short MaxConcurrentRequests = 50;
    
    private readonly Engine _engine;
    private readonly WithdrawalStore _withdrawalStore;
    private readonly CustomerStore _customerStore;
    private readonly ApprovalRuleStore _approvalRuleStore;

    public RunApprovalRulesUseCase(WithdrawalStore withdrawalStore, CustomerStore customerStore, ApprovalRuleStore approvalRuleStore, Engine engine) {
        _withdrawalStore = withdrawalStore;
        _customerStore = customerStore;
        _approvalRuleStore = approvalRuleStore;
        _engine = engine;
    }

    public async Task<RunApprovalRulesResponse> ExecuteAsync(IEnumerable<long> withdrawalGroups, CancellationToken cancellationToken) {
        var inputEnum = await _withdrawalStore.GetWithdrawalGroupsAsync(withdrawalGroups, cancellationToken);
        
        var inputs = await inputEnum.Select(g => MapInputAsync(g, cancellationToken))
                                    .WhenAll(MaxConcurrentRequests);
                                         
        var results = await inputs.Select(RunRules)
                                  .WhenAll(MaxConcurrentRequests);

        var outcomes = results.SelectMany(i => i)
                              .Select(MapToOutcome)
                              .ToList();

        await AddRulesAsync(outcomes, cancellationToken);
        
        return new RunApprovalRulesResponse { Outcomes = outcomes };
        
        Task<IEnumerable<Result>> RunRules(RuleInput input) 
            => _engine.ExecuteAsync("approval", input, cancellationToken);
    }

    private async Task<RuleInput> MapInputAsync(WithdrawalGroup withdrawalGroup, CancellationToken cancellationToken) {
        var withdrawalEnum = await _withdrawalStore.GetWithdrawalsAsync(withdrawalGroup.WithdrawalIds, cancellationToken);
        var withdrawals = withdrawalEnum.ToList();
        var customers = await _customerStore.GetCustomersAsync(new []{withdrawals.First().CustomerId}, cancellationToken);
        var customer = customers.FirstOrDefault();

        return new RuleInput {
            WithdrawalGroupId = withdrawalGroup.Id,
            CustomerId = customer?.Id??0,
            Balance = customer?.Balance??0M,
            Amount = withdrawals.Sum(w => w.Amount)
        };
    }

    private static ApprovalRuleOutcome MapToOutcome(Result result) =>
        new() {
            WithdrawalGroupId = ((RuleInput)result.Input!).WithdrawalGroupId,
            RuleName = result.Name,
            IsSuccessful = IsSuccessful(result),
            Message = result.Message??string.Empty
        };

    private static bool IsSuccessful(Result result) {
        var isOutputSuccessful = result.Output as ActionResult?? new ActionResult{Output = true};
        return result.IsSuccessful && (bool) isOutputSuccessful.Output;
    }

    private async Task AddRulesAsync(IEnumerable<ApprovalRuleOutcome> outcomes, CancellationToken cancellationToken) {
        var rules = outcomes.GroupBy(outcome => outcome.WithdrawalGroupId, GetRuleHistory);
        await _approvalRuleStore.AddRuleHistoriesAsync(rules, cancellationToken);
    }

    private static Data.ApprovalRuleHistory GetRuleHistory(long withdrawalGroupId, IEnumerable<ApprovalRuleOutcome> outcomes) =>
        new() {
            WithdrawalGroupId = withdrawalGroupId,
            TransactionId =  Guid.NewGuid(),
            TransactionDate = DateTime.Now,
            Rules = outcomes.Select(MapRule).ToList(),
            Metadata = new List<SharedData.MetaData>{new(){ Name = "withdrawal-id", Value = $"{withdrawalGroupId}"}}
        };

    private static Data.ApprovalRule MapRule(ApprovalRuleOutcome outcome) =>
        new() {
            RuleName = outcome.RuleName,
            Message = outcome.Message,
            IsSuccessful = outcome.IsSuccessful
        };
}