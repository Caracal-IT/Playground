using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Playground.Rules.CustomActions.Terminal;
using RulesEngine.Actions;
using Xunit;

using RulesEngine.Models;

// ReSharper disable InconsistentNaming

namespace Playground.Rules.Tests {
    public static class Utils
    {
        public static bool CheckContains(WithdrawalRequest check, int val) {
            return check.Amount > val;
        }
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
}

/*
 
 "Actions": [
            {
              "OnSuccess": {
                "Name": "OutputExpression",
                "Context": [
                  {"Key":  "Expression", "Value":  "input1.amount * 5000"}
                ]
              }
            }
          ]
 
 
 
 using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caracal.Framework.UseCases;
using Caracal.PayStation.Payments;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Actions;
using RulesEngine.Extensions;
using RulesEngine.Models;

namespace Caracal.PayStation.Application.UseCases.Withdrawals.AutoAllocate {
    public class AutoAllocateUseCase: UseCase<AutoAllocateResponse, AutoAllocateRequest> {
        private readonly WithdrawalService _service;
        private readonly IFileProvider _fileProvider;
        
        public AutoAllocateUseCase(WithdrawalService service, IFileProvider fileProvider) {
            _service = service;
            _fileProvider = fileProvider;
        }
        
        public override async Task<AutoAllocateResponse> ExecuteAsync(AutoAllocateRequest request, CancellationToken cancellationToken) {
            var basicInfo = "{\"name\": \"hello\",\"email\": \"abcy@xyz.com\",\"creditHistory\": \"good\",\"country\": \"canada\",\"loyalityFactor\": 3,\"totalPurchasesToDate\": 10000}";
            var orderInfo = "{\"totalOrders\": 5,\"recurringItems\": 2}";
            var telemetryInfo = "{\"noOfVisitsPerMonth\": 10,\"percentageOfBuyingToVisit\": 15}";

            var converter = new ExpandoObjectConverter();

            dynamic? input1 = JsonConvert.DeserializeObject<ExpandoObject>(basicInfo, converter);
            dynamic? input2 = JsonConvert.DeserializeObject<ExpandoObject>(orderInfo, converter);
            dynamic? input3 = JsonConvert.DeserializeObject<ExpandoObject>(telemetryInfo, converter);

            var inputs = new []
            {
                input1,
                input2,
                input3
            };
            
            var workflowRules = JsonConvert.DeserializeObject<List<WorkflowRules>>(await GetRulesAsync());
            var reSettingsWithCustomTypes = new ReSettings { CustomTypes = new [] { typeof(Utils) } };
            var bre = new RulesEngine.RulesEngine(workflowRules.ToArray(), null, reSettingsWithCustomTypes);
            
            string discountOffered = "No discount offered.";

            List<RuleResultTree> resultList = await bre.ExecuteAllRulesAsync("Discount", inputs);

            resultList.OnSuccess((eventName) => discountOffered = $"Discount offered is {eventName} % over MRP.");

            resultList.OnFail(() => {
                discountOffered = "The user is not eligible for any discount.";
            });

            return new AutoAllocateResponse {
                Result = discountOffered
            };
        }
        
        private async Task<string> GetRulesAsync() {
            var dir = _fileProvider.GetDirectoryContents(Path.Join("Workflow", "Rules"));
            var file = dir.FirstOrDefault(f => f.Name.Equals("discount.rule.json"));

            if (file == null)
                return string.Empty;
            
            var contents = new StreamReader(file.CreateReadStream());
            return await contents.ReadToEndAsync();
        }
    }
}
*/