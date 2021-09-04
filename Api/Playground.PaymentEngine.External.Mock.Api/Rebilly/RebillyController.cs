using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.External.Mock.Api.Rebilly {
    [ApiController]
    [Route("[controller]")]
    public class RebillyController: ControllerBase {
        [HttpPost("Process")]
        public RBProcessResponse Process([FromBody] RBProcessRequest request) {
            var authUser = Request.Headers["auth-user"].ToString();
            var apiKey = Request.Headers["api-key"].ToString();
            
            return new RBProcessResponse {
                Name = $"{nameof(RebillyController)} Auth User : {authUser}, API Key : {apiKey}",
                Reference = request.Reference,
                Code = request.Code,
                Amount = request.Amount
            } ;
        }
        
        [HttpPost("Callback")]
        public RBCallbackResponse Callback([FromBody] RBCallbackRequest request) {
            return new RBCallbackResponse {
                Name = nameof(RebillyController),
                Reference = request.Reference,
                Code = request.Code
            } ;
        }
    }
}