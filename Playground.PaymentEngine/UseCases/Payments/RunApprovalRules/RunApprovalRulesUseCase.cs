using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.Rules;
using RulesEngine.Models;
using Rule = Playground.PaymentEngine.Model.Rule;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesUseCase {
        private readonly Engine _engine;
        private readonly PaymentStore _store;

        public RunApprovalRulesUseCase(PaymentStore store, Engine engine) {
            _store = store;
            _engine = engine;
        }

        public async Task<RunApprovalRulesResponse> ExecuteAsync(RunApprovalRulesRequest request, CancellationToken cancellationToken) {
            var results = await _store.GetWithdrawals(request.Withdrawals)
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

        private static RuleInput MapInput(Withdrawal withdrawal) => 
            new() {
                WithdrawalId = withdrawal.Id,
                CustomerId = withdrawal.CustomerId,
                Balance = withdrawal.Balance,
                Amount = withdrawal.Amount
            };
        
        private static ApprovalRuleOutcome MapToOutcome(Result result) =>
            new() {
                WithdrawalId = ((RuleInput)result.Input)!.WithdrawalId,
                RuleName = result.Name,
                IsSuccessful = IsSuccessful(result),
                Message = result.Message
            };

        private static bool IsSuccessful(Result result) {
            var isOutputSuccessful = result.Output as ActionResult?? new ActionResult{Output = true};
            return result.IsSuccessful && (bool) isOutputSuccessful.Output;
        }

        private void AddRules(IEnumerable<ApprovalRuleOutcome> outcomes) {
            var rules = outcomes.GroupBy(outcome => outcome.WithdrawalId, GetRuleHistory);
            _store.AddRuleHistories(rules);
        }

        private static RuleHistory GetRuleHistory(long withdrawalId, IEnumerable<ApprovalRuleOutcome> outcomes) =>
            new() {
                WithdrawalId = withdrawalId,
                TransactionId =  Guid.NewGuid(),
                TransactionDate = DateTime.Now,
                Rules = outcomes.Select(MapRule).ToList(),
                Metadata = new List<MetaData>{ new(){ Name = "withdrawal-id", Value = $"{withdrawalId}"}}
            };

        private static Rule MapRule(ApprovalRuleOutcome outcome) =>
            new Rule {
                RuleName = outcome.RuleName,
                Message = outcome.Message,
                IsSuccessful = outcome.IsSuccessful
            };
    }
}