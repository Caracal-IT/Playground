using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RulesEngine.Actions;
using RulesEngine.Models;


namespace Playground.Rules {
    public class Engine {
        private readonly RuleStore _ruleStore;
        
        public Engine(RuleStore ruleStore) {
            _ruleStore = ruleStore;
        }

        public Task<IEnumerable<Result>> ExecuteAsync<T>(
            string workflowName, 
            T request, 
            Dictionary<string, Func<ActionBase>> customActions, 
            CancellationToken cancellationToken) 
            => ExecuteAsync(workflowName, request, cancellationToken, null, customActions);
        
        public Task<IEnumerable<Result>> ExecuteAsync<T>(
            string workflowName, 
            T request, 
            CancellationToken cancellationToken,
            IEnumerable<Type>? customTypes = null, 
            Dictionary<string, Func<ActionBase>>? customActions = null)
            => TryExecuteRulesAsync(workflowName, request, customTypes, customActions, cancellationToken);

        private async Task<IEnumerable<Result>> TryExecuteRulesAsync<T>(
            string workflowName,
            T request,
            IEnumerable<Type>? customTypes,
            Dictionary<string, Func<ActionBase>>? customActions,
            CancellationToken cancellationToken) 
        {
            try { return await ExecuteRulesAsync(workflowName, request, customTypes, customActions, cancellationToken); }
            catch (Exception ex) { return new[] { new Result { Message = ex.Message, IsSuccessful = false } }; }
        }
        
        private async Task<IEnumerable<Result>> ExecuteRulesAsync<T>(
            string workflowName, 
            T request,
            IEnumerable<Type>? customTypes, 
            Dictionary<string, Func<ActionBase>>? customActions,
            CancellationToken cancellationToken) 
        {
            var workflow = await _ruleStore.GetRulesAsync(workflowName, cancellationToken);
            var reSettings = new ReSettings {
                CustomTypes = customTypes?.ToArray(),
                CustomActions = customActions
            };
            
            var engine = new RulesEngine.RulesEngine(workflow.ToArray(), null, reSettings);
            var result = await engine.ExecuteAllRulesAsync(workflowName, request);

            return result.Select(r => MapResult(request, r));
        }

        private static Result MapResult(object? request, RuleResultTree result) {
            var actionResult = result.ActionResult?.Output is not bool || Convert.ToBoolean(result.ActionResult);
            var isSuccessful = result.IsSuccess && actionResult;
            var message = isSuccessful ? result.Rule.SuccessEvent : result.Rule.ErrorMessage;
            var hasException = !string.IsNullOrWhiteSpace(result.ExceptionMessage) || result.ActionResult?.Exception != null;
            var exceptionMessage = result.ActionResult?.Exception?.Message ?? result.ExceptionMessage;
            
            return new Result {
                Name = result.Rule.RuleName,
                IsSuccessful = isSuccessful && !hasException,
                Message = hasException ? exceptionMessage : message,
                Input = request,
                Output = result.ActionResult?.Output
            };
        }
    }
}