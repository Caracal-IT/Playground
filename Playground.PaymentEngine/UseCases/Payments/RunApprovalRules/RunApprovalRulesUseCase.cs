using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.Rules;
using RulesEngine.Models;

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
    }
}