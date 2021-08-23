using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.Stores.Customers;
using Playground.PaymentEngine.Stores.Model;
using Playground.PaymentEngine.Stores.Withdrawals;
using Playground.PaymentEngine.Stores.Withdrawals.Model;
using Playground.Rules;
using RulesEngine.Models;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesUseCase {
        private readonly Engine _engine;
        private readonly WithdrawalStore _paymentStore;
        private readonly CustomerStore _customerStore;
        private readonly ApprovalRuleStore _approvalRuleStore;

        public RunApprovalRulesUseCase(WithdrawalStore paymentStore, CustomerStore customerStore, ApprovalRuleStore approvalRuleStore, Engine engine) {
            _paymentStore = paymentStore;
            _customerStore = customerStore;
            _approvalRuleStore = approvalRuleStore;
            _engine = engine;
        }

        public async Task<RunApprovalRulesResponse> ExecuteAsync(RunApprovalRulesRequest request, CancellationToken cancellationToken) {
            var results = await _paymentStore.GetWithdrawalGroups(request.WithdrawalGroups)
                                             .Select(MapInput)
                                             .Select(RunRules)
                                             .WhenAll(50);

            var outcomes = results.SelectMany(i => i)
                                  .Select(MapToOutcome)
                                  .ToList();

            await AddRulesAsync(outcomes, cancellationToken);
            
            return new RunApprovalRulesResponse { Outcomes = outcomes };
            
            Task<IEnumerable<Result>> RunRules(RuleInput input) 
                =>_engine.ExecuteAsync("approval", input, cancellationToken);
        }

        private RuleInput MapInput(WithdrawalGroup withdrawalGroup) {
            var withdrawals = _paymentStore.GetWithdrawals(withdrawalGroup.WithdrawalIds).ToList();
            var customer = _customerStore.GetCustomer(withdrawals.First().CustomerId);

            return new() {
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