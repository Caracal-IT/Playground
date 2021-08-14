using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.Rebilly {
    [ApiController]
    [Route("[controller]")]
    public class RebillyController: ControllerBase {
        [HttpPost("Process")]
        public RBProcessResponse Process([FromBody] RBProcessRequest request) {
            return new RBProcessResponse {
                Reference = request.Reference,
                Code = "00"
            } ;
        }
    }

    public class RBProcessRequest {
        public string Reference { get; set; }
    }

    public class RBProcessResponse {
        public string Reference { get; set; }
        public string Code { get; set; }
    }
}