using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.PayPal {
    [ApiController]
    [Route("[controller]")]
    public class PayPalController: ControllerBase {
        [HttpPost("Process")]
        public PPProcessResponse Process([FromBody] PPProcessRequest request) {
            return new PPProcessResponse {
                Reference = request.Reference,
                Code = "00"
            } ;
        }
    }

    public class PPProcessRequest {
        public string Reference { get; set; }
    }

    public class PPProcessResponse {
        public string Reference { get; set; }
        public string Code { get; set; }
    }
}