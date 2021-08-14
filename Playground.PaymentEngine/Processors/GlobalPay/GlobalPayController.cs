using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.GlobalPay {
    [ApiController]
    [Route("[controller]")]
    public class GlobalPayController: ControllerBase {
        [HttpPost("Process")]
        public GPProcessResponse Process([FromBody] GPProcessRequest request) {
            return new GPProcessResponse {
                Reference = request.Reference,
                Code = "00"
            } ;
        }
    }

    public class GPProcessRequest {
        public string Reference { get; set; }
    }

    public class GPProcessResponse {
        public string Reference { get; set; }
        public string Code { get; set; }
    }
}