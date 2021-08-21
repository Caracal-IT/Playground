using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.Rules;

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
                Balance = withdrawal.Balance,
                Amount = withdrawal.Amount
            };
        
        private static ApprovalRuleOutcome MapToOutcome(Result result) =>
            new() {
                WithdrawalId = ((RuleInput) result.Input)!.WithdrawalId,
                RuleName = result.Name,
                IsSuccessful = result.IsSuccessful,
                Message = result.Message
            };
    }
}