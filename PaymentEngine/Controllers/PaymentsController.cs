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
using PaymentEngine.Model;
using PaymentEngine.Stores;
using PaymentEngine.UseCases.Payments.Callback;
using PaymentEngine.UseCases.Payments.Process;

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

        [HttpPost("process/xml/callback/{reference}")]
        [Produces("application/xml")]
        public async Task<CallbackResponse> ProcessXmlCallback([FromServices] CallbackUseCase useCase, [FromRoute] string reference, CancellationToken token) {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var request = new CallbackRequest {
                Data = body.Trim().ReplaceLineEndings(string.Empty),
                Reference = reference
            };
            
            return await useCase.ExecuteAsync(request, token);
        }
        
        [HttpPost("process/callback/{reference}")]
        public async Task<CallbackResponse> ProcessCallback([FromServices] CallbackUseCase useCase, [FromRoute] string reference, [FromBody] JsonElement payload, CancellationToken token) {
            var xml = JsonConvert.DeserializeXNode(payload.GetRawText(), "root")
                                 .ToString(SaveOptions.DisableFormatting);
            
            var request = new CallbackRequest {
                Data = xml.Substring(6, xml.Length - 13),
                Reference = reference
            };
            
            return await useCase.ExecuteAsync(request, token);
        }
    }

    [XmlRoot("callback")]
    public class Callback {
        [XmlElement("ref")]
        public string Reference { get; set; }
        [XmlElement("status")]
        public string Status { get; set; }
    }
}