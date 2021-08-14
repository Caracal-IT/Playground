using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PaymentEngine.Model;
using PaymentEngine.Stores;
using PaymentEngine.UseCases.Payments.Callback;
using PaymentEngine.UseCases.Payments.Process;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace PaymentEngine.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController: ControllerBase {
        private readonly PaymentStore _paymentStore;
        
        public PaymentsController(PaymentStore paymentStore) => _paymentStore = paymentStore;
        
        [HttpGet("store")]
        public Store GetStore() => _paymentStore.GetStore();
        
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
            
            var resp = await useCase.ExecuteAsync(request, token);
            if (string.IsNullOrWhiteSpace(resp.Response)) return null;

            var resp2 = JsonConvert.SerializeXNode(XDocument.Parse(resp.Response)).Replace("@", "");
            var resp3 = XDocument.Parse(resp.Response).Root;
            return resp3;
        }
        
        [HttpPost("process/json/{method}/{reference}")]
        public async Task<object> ProcessCallback([FromServices] CallbackUseCase useCase, [FromRoute] string method, [FromRoute] string reference, [FromBody] JsonElement payload, CancellationToken token) {
            var xml = JsonConvert.DeserializeXNode(payload.GetRawText(), "root")
                                 .ToString(SaveOptions.DisableFormatting);
            
            var request = new CallbackRequest {
                Action = method.ToLower(),
                Data = xml.Substring(6, xml.Length - 13),
                Reference = reference
            };

            var resp = await useCase.ExecuteAsync(request, token);
            if (string.IsNullOrWhiteSpace(resp.Response)) return null;
            
            var resp2 = JsonConvert.SerializeXNode(XDocument.Parse(resp.Response)).Replace("@", "");
            var resp3 = System.Text.Json.JsonSerializer.Deserialize<object>(resp2);
            return resp3;
        }
    }
}