using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.PayPal {
    [ApiController]
    [Route("[controller]")]
    public class PayPalController: ControllerBase {
        [HttpPost("Process")]
        public PPProcessResponse Process([FromBody] PPProcessRequest request) {
            return new PPProcessResponse {
                Name = nameof(PayPalController),
                Reference = request.Reference,
                Code = request.Code,
                Amount = request.Amount
            } ;
        }
    }
}