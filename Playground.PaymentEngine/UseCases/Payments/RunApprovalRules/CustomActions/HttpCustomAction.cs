using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Playground.PaymentEngine.Processors.Compliance;
using RulesEngine.Actions;
using RulesEngine.Models;

namespace Playground.PaymentEngine.UseCases.Payments.RunApprovalRules.CustomActions {
    public class HttpCustomAction : ActionBase {
        private readonly HttpClient _httpClient;

        public HttpCustomAction(HttpClient httpClient) {
            _httpClient = httpClient;
        }
        
        public override async ValueTask<object> Run(ActionContext context, RuleParameter[] ruleParameters) {
            var customInput = (RuleInput) ruleParameters.First().Value;
            var request = new ValidateCustomerRequest {
                Amount = customInput.Amount,
                CustomerId = customInput.CustomerId
            };

            var httpResponse = await _httpClient.PostAsJsonAsync("https://localhost:5001/Compliance/Fraud/Validate", request);
            var response = await httpResponse.Content.ReadFromJsonAsync<ValidateCustomerResponse>()??new ValidateCustomerResponse();

            return new ActionResult {
                Output = httpResponse.IsSuccessStatusCode && response.IsValid
            };
        }
    }
}

/*
 * public class MyCustomAction : ActionBase {

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
*/