using System;
using System.Linq;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.Stores.Customers;
using Playground.PaymentEngine.Stores.Model;
using Playground.PaymentEngine.Stores.Withdrawals;
using Playground.PaymentEngine.Stores.Withdrawals.Model;
using Playground.Rules;

using ActionResult = RulesEngine.Models.ActionResult;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesUseCase {
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

        public async Task<RunApprovalRulesResponse> ExecuteAsync(RunApprovalRulesRequest request, CancellationToken cancellationToken) {
            var inputEnum = await _withdrawalStore.GetWithdrawalGroupsAsync(request.WithdrawalGroups, cancellationToken);
            var inputs = await inputEnum.Select(g => MapInputAsync(g, cancellationToken))
                                        .WhenAll(50);
                                             
            var results = await inputs.Select(RunRules).WhenAll(50);

            var outcomes = results.SelectMany(i => i)
                                  .Select(MapToOutcome)
                                  .ToList();

            await AddRulesAsync(outcomes, cancellationToken);
            
            return new RunApprovalRulesResponse { Outcomes = outcomes };
            
            Task<IEnumerable<Result>> RunRules(RuleInput input) 
                =>_engine.ExecuteAsync("approval", input, cancellationToken);
        }

        private async Task<RuleInput> MapInputAsync(WithdrawalGroup withdrawalGroup, CancellationToken cancellationToken) {
            var withdrawalEnum = await _withdrawalStore.GetWithdrawalsAsync(withdrawalGroup.WithdrawalIds, cancellationToken);
            var withdrawals = withdrawalEnum.ToList();
            var customer = await _customerStore.GetCustomerAsync(withdrawals.First().CustomerId, cancellationToken);

            return new RuleInput {
                WithdrawalGroupId = withdrawalGroup.Id,
                CustomerId = customer.Id,
                Balance = customer.Balance,
                Amount = withdrawals.Sum(w => w.Amount)
            };
        }

        private static ApprovalRuleOutcome MapToOutcome(Result result) =>
            new() {
                WithdrawalGroupId = ((RuleInput)result.Input)!.WithdrawalGroupId,
                RuleName = result.Name,
                IsSuccessful = IsSuccessful(result),
                Message = result.Message
            };

        private static bool IsSuccessful(Result result) {
            var isOutputSuccessful = result.Output as ActionResult?? new ActionResult{Output = true};
            return result.IsSuccessful && (bool) isOutputSuccessful.Output;
        }

        private async Task AddRulesAsync(IEnumerable<ApprovalRuleOutcome> outcomes, CancellationToken cancellationToken) {
            var rules = outcomes.GroupBy(outcome => outcome.WithdrawalGroupId, GetRuleHistory);
            await _approvalRuleStore.AddRuleHistoriesAsync(rules, cancellationToken);
        }

        private static ApprovalRuleHistory GetRuleHistory(long withdrawalGroupId, IEnumerable<ApprovalRuleOutcome> outcomes) =>
            new() {
                WithdrawalGroupId = withdrawalGroupId,
                TransactionId =  Guid.NewGuid(),
                TransactionDate = DateTime.Now,
                Rules = outcomes.Select(MapRule).ToList(),
                Metadata = new List<MetaData>{ new(){ Name = "withdrawal-id", Value = $"{withdrawalGroupId}"}}
            };

        private static ApprovalRule MapRule(ApprovalRuleOutcome outcome) =>
            new() {
                RuleName = outcome.RuleName,
                Message = outcome.Message,
                IsSuccessful = outcome.IsSuccessful
            };
    }
}