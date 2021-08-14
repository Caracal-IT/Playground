using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.Rebilly {
    [ApiController]
    [Route("[controller]")]
    public class RebillyController: ControllerBase {
        [HttpPost("Process")]
        public RBProcessResponse Process([FromBody] RBProcessRequest request) {
            return new RBProcessResponse {
                Name = nameof(RebillyController),
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