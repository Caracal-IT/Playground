namespace Playground.PaymentEngine.External.Mock.Api.GlobalPay;

[ApiController]
[Route("[controller]")]
public class GlobalPayController: ControllerBase {
    [HttpPost("Process")]
    public GPProcessResponse Process([FromBody] GPProcessRequest request) {
        return new GPProcessResponse {
            Name = nameof(GlobalPayController),
            Reference = request.Reference,
            Code = request.Code,
            Amount = request.Amount
        } ;
    }
}