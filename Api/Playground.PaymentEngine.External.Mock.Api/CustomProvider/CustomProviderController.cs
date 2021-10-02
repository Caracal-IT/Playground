namespace Playground.PaymentEngine.External.Mock.Api.CustomProvider;

[ApiController]
[Route("[controller]")]
public class CustomProviderController: ControllerBase {
    [HttpPost("Process")]
    [Produces("application/xml")]
    public PPProcessResponse Process2([FromBody] PPProcessRequest request) {
        return new PPProcessResponse {
            Name = nameof(CustomProviderController),
            Reference = request.Reference,
            Code = request.Code,
            Amount = request.Amount
        } ;
    }
}