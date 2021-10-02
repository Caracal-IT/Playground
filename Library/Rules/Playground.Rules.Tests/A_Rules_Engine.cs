// ReSharper disable InconsistentNaming
namespace Playground.Rules.Tests;

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Playground.PaymentEngine.Rules.Store;
using RulesEngine.Actions;
using Xunit;

using RulesEngine.Models;

public static class Utils
{
    public static bool CheckContains(WithdrawalRequest check, int val) => 
        check.Amount > val;
}

public class WithdrawalRequest {
    public string Name { get; set; }
    public decimal Amount { get; set; }
}


public class MyCustomAction : ActionBase {

    public MyCustomAction(string someInput) { }

    public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters) {
        await Task.Delay(200);
        var customInput = context.GetContext<string>("customContextInput");
        var test = ruleParameters.LastOrDefault()?.Value as WithdrawalRequest;
        if(test != null)
            test.Amount = 906;

        //Add your custom logic here and return a ValueTask
        return "sdfsdfsdf 45645646";
    }
}

public class FileRuleStore: RuleStore {
    private string _basePath;

    public FileRuleStore() {
        _basePath = "Rules";
    }
    
    public async Task<IEnumerable<WorkflowRules>> GetRulesAsync(string name, CancellationToken cancellationToken) {
        var json = await File.ReadAllTextAsync(Path.Combine(_basePath, $"{name}.json"));
        return JsonConvert.DeserializeObject<List<WorkflowRules>>(json);
    }
}

public class A_Rules_Engine {

   [Fact]
   public async Task Should_Run_Rules() {
       var engine = new Engine(new FileRuleStore(), null);
       
       var request = new WithdrawalRequest { Name = "Request 1", Amount = 10 };

       var customTypes = new[] { typeof(Utils) };
       var customActions = new Dictionary<string, Func<ActionBase>> {
           { "MyCustomAction", () => new MyCustomAction("Testing") }
       };
       
       var result = await engine.ExecuteAsync("default", request, CancellationToken.None, customTypes, customActions);
      
       Assert.Equal(5, result.Count());
   }
}