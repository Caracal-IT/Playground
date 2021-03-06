namespace Playground.PaymentEngine.Api.Controllers; 

using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using Application.UseCases.Payments.Callback;
using Application.UseCases.Payments.Process;
using Xml;

using ExportResponse = Models.Payments.ExportResponse;
using ProcessRequest = Application.UseCases.Payments.Process.ProcessRequest;
using ViewModel = Models.Payments;

[ApiController]
[Route("payments")]
public class PaymentController: ControllerBase {
    private readonly IMapper _mapper;
    
    public PaymentController(IMapper mapper) => 
        _mapper = mapper;
    
    [HttpPost("process")]
    public async Task<ActionResult<IEnumerable<ExportResponse>>> ProcessAsync([FromServices] ProcessUseCase useCase, ProcessRequest request, CancellationToken cancellationToken) => 
        await ExecuteAsync<ActionResult<IEnumerable<ExportResponse>>>(async () => {
            var response = await useCase.ExecuteAsync(request, cancellationToken);
            return Ok(_mapper.Map<IEnumerable<ExportResponse>>(response));
        });

    [HttpPost("process/xml/{method}/{reference}")]
    [Produces("application/xml")]
    public async Task<ActionResult<object>> ProcessXmlCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();

            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = body.Trim().ReplaceLineEndings(string.Empty),
                Reference = reference
            };

            var response = await useCase.ExecuteAsync(request, cancellationToken);
            return Ok(string.IsNullOrWhiteSpace(response.Response) ? null : XDocument.Parse(response.Response).Root);
        });

    [HttpPost("process/json/{method}/{reference}")]
    public async Task<ActionResult<object>> ProcessCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, [FromBody] JsonElement payload, CancellationToken cancellationToken) =>
        await ExecuteAsync(async () => {
            var xml = payload.GetRawText().ToXml("root");

            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = xml.Substring(6, xml.Length - 13),
                Reference = reference
            };

            var response = await useCase.ExecuteAsync(request, cancellationToken);
            return response.Response.ToJson();
        });
}