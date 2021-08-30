using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Playground.PaymentEngine.Stores.ApprovalRules;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;
using Playground.Xml;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("payments")]
    public class PaymentController: ControllerBase {
        [HttpPost("process")]
        public async Task<ProcessResponse> ProcessAsync([FromServices] ProcessUseCase useCase, ProcessRequest request, CancellationToken cancellationToken) => 
            await useCase.ExecuteAsync(request, cancellationToken);

        [HttpPost("process/xml/{method}/{reference}")]
        [Produces("application/xml")]
        public async Task<object> ProcessXmlCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, CancellationToken cancellationToken) {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            
            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = body.Trim().ReplaceLineEndings(string.Empty),
                Reference = reference
            };
            
            var response = await useCase.ExecuteAsync(request, cancellationToken);
            return string.IsNullOrWhiteSpace(response.Response) ? null : XDocument.Parse(response.Response).Root;
        }
        
        [HttpPost("process/json/{method}/{reference}")]
        public async Task<object> ProcessCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, [FromBody] JsonElement payload, CancellationToken cancellationToken) {
            var xml = payload.GetRawText()
                             .ToXml("root");
            
            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = xml.Substring(6, xml.Length - 13),
                Reference = reference
            };

            var response = await useCase.ExecuteAsync(request, cancellationToken);
            return response.Response.ToJson();
        }
    }
}