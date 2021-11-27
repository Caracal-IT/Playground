namespace Playground.PaymentEngine.External.Mock.Api.Compliance;

[ApiController]
[Route("Compliance/[controller]")]
public class FraudController: ControllerBase {
    [HttpPost("Validate")]
    public ValidateCustomerResponse ValidateCustomer(ValidateCustomerRequest request, CancellationToken cancellationToken) =>
        new() {
            IsValid = request.CustomerId != 74
        };
}