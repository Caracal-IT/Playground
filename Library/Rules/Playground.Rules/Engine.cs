using System.Collections.Generic;
using Playground.PaymentEngine.Rules.Store;
using Playground.Rules.CustomActions.Terminal;
using RulesEngine.Actions;
using RulesEngine.Models;

namespace Playground.Rules;

public class Engine {
    private readonly RuleStore _ruleStore;
    private readonly TerminalAction? _terminalAction;
    private readonly Dictionary<string, Func<ActionBase>> _customActions;
    
    public Engine(RuleStore ruleStore, TerminalAction? terminalAction) {
        _ruleStore = ruleStore;
        _terminalAction = terminalAction;
        _customActions = GetCustomActions();
    }
    
    public Task<IEnumerable<Result>> ExecuteAsync<T>(
        string workflowName, 
        T request, 
        CancellationToken cancellationToken,
        IEnumerable<Type>? customTypes = null, 
        Dictionary<string, Func<ActionBase>>? customActions = null)
        => TryExecuteRulesAsync(workflowName, request, customTypes, customActions, cancellationToken);

    private Dictionary<string, Func<ActionBase>> GetCustomActions() {
        var actions = new Dictionary<string, Func<ActionBase>>();
        
        if(_terminalAction != null)
            actions.Add("TerminalAction", () => _terminalAction );
       
        return actions;
    }
    
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

        var actions = new Dictionary<string, Func<ActionBase>>(_customActions);
        
        if(customActions != null)
            foreach (var keyValuePair in customActions) 
                actions.TryAdd(keyValuePair.Key, keyValuePair.Value);

        var reSettings = new ReSettings {
            CustomTypes = customTypes?.ToArray(),
            CustomActions = customActions??_customActions
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