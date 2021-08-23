using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.Stores.CustomerStores;
using Playground.PaymentEngine.Stores.PaymentStores;
using Playground.Rules;
using RulesEngine.Models;
using Rule = Playground.PaymentEngine.Model.Rule;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesUseCase {
        private readonly Engine _engine;
        private readonly PaymentStore _paymentStore;
        private readonly CustomerStore _customerStore;

        public RunApprovalRulesUseCase(PaymentStore paymentStore, CustomerStore customerStore, Engine engine) {
            _paymentStore = paymentStore;
            _customerStore = customerStore;
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

            AddRules(outcomes);
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

        private void AddRules(IEnumerable<ApprovalRuleOutcome> outcomes) {
            var rules = outcomes.GroupBy(outcome => outcome.WithdrawalGroupId, GetRuleHistory);
            _paymentStore.AddRuleHistories(rules);
        }

        private static RuleHistory GetRuleHistory(long withdrawalGroupId, IEnumerable<ApprovalRuleOutcome> outcomes) =>
            new() {
                WithdrawalGroupId = withdrawalGroupId,
                TransactionId =  Guid.NewGuid(),
                TransactionDate = DateTime.Now,
                Rules = outcomes.Select(MapRule).ToList(),
                Metadata = new List<MetaData>{ new(){ Name = "withdrawal-id", Value = $"{withdrawalGroupId}"}}
            };

        private static Rule MapRule(ApprovalRuleOutcome outcome) =>
            new Rule {
                RuleName = outcome.RuleName,
                Message = outcome.Message,
                IsSuccessful = outcome.IsSuccessful
            };
    }
}