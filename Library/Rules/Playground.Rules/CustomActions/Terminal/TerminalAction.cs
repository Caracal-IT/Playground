using Playground.Router.Services;
using Playground.Xml;
using RulesEngine.Actions;
using RulesEngine.Models;

using static Playground.Xml.Serialization.Serializer;

namespace Playground.Rules.CustomActions.Terminal;

public class TerminalAction : ActionBase {
    private readonly IRoutingService _routingService;

    public TerminalAction(IRoutingService routingService) => 
        _routingService = routingService;

    public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters) {
        var terminal = context.GetContext<string>("terminal");
        var action = context.GetContext<string>("action");
        
        var customInput = ruleParameters.First().Value;
        var routeReq = new RoutingRequest(Guid.NewGuid(), action, customInput.ToXml(), new []{ terminal });

        var routeResponse = await _routingService.SendAsync(routeReq, CancellationToken.None);
        var lastResponse = routeResponse.LastOrDefault();
        
        if(!(lastResponse?.TerminalResponse.Success??false) || string.IsNullOrWhiteSpace(lastResponse?.Result))
            return new ActionResult { Output = false };
        
        var response = DeSerialize<TerminalResult>(lastResponse.Result);
        
        return new ActionResult { Output = response?.IsValid??false };
    }
}