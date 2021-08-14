using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.PaymentEngine.UseCases.Payments.Callback;
using Playground.PaymentEngine.UseCases.Payments.Process;
using Playground.Xml;
using static System.Text.Json.JsonSerializer;
using static Newtonsoft.Json.JsonConvert;

namespace Playground.PaymentEngine.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController: ControllerBase {
        private readonly PaymentStore _paymentStore;
        public PaymentsController(PaymentStore paymentStore) => _paymentStore = paymentStore;
        
        [HttpGet("allocations")]
        public List<Allocation> GetAllocations() => _paymentStore.GetStore().Allocations.AllocationList;

        [HttpPost("auto-allocate")]
        public List<Allocation> AutoAllocate(ProcessRequest request) {
            var store = _paymentStore.GetStore();
            var allocations = store.Allocations.AllocationList.Where(a => request.Allocations.Contains(a.Id)).ToList();

            allocations.ForEach(a => _paymentStore.SetAllocationStatus(a.Id, 1));
            
            return allocations;
        }

        [HttpPost("process")]
        public async Task<ProcessResponse> ProcessAsync([FromServices] ProcessUseCase useCase, ProcessRequest request, CancellationToken token) => 
            await useCase.ExecuteAsync(request, token);

        [HttpPost("process/xml/{method}/{reference}")]
        [Produces("application/xml")]
        public async Task<object> ProcessXmlCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, CancellationToken token) {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            
            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = body.Trim().ReplaceLineEndings(string.Empty),
                Reference = reference
            };
            
            var response = await useCase.ExecuteAsync(request, token);
            return response.Response.ToXml();
        }
        
        [HttpPost("process/json/{method}/{reference}")]
        public async Task<object> ProcessCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, [FromBody] JsonElement payload, CancellationToken token) {
            var xml = DeserializeXNode(payload.GetRawText(), "root").ToString(SaveOptions.DisableFormatting);
            
            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = xml.Substring(6, xml.Length - 13),
                Reference = reference
            };

            var response = await useCase.ExecuteAsync(request, token);
            return response.Response.ToJson();
        }
    }
}