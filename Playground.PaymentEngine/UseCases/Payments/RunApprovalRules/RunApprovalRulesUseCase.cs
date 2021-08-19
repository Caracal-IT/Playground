using System.Threading;
using System.Threading.Tasks;
using Playground.Rules;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules {
    public class RunApprovalRulesUseCase {
        private readonly Engine _engine;

        public RunApprovalRulesUseCase(Engine engine) =>
            _engine = engine;
        
        public async Task<RunApprovalRulesResponse> ExecuteAsync(RunApprovalRulesRequest request, CancellationToken cancellationToken) {
            var req = new {
                Balance = 300,
                Amount = 10
            };

            var results = await _engine.ExecuteAsync("approval", req, cancellationToken);
            
            return new RunApprovalRulesResponse();
        } 
    }
}