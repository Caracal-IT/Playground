using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace Playground.PaymentEngine.Processors.Compliance {
    [ApiController]
    [Route("Compliance/[controller]")]
    public class FraudController: ControllerBase {
        [HttpPost("Validate")]
        public ValidateCustomerResponse ValidateCustomer(ValidateCustomerRequest request, CancellationToken cancellationToken) =>
            new ValidateCustomerResponse {
                IsValid = request.CustomerId != 74
            };
    }
}