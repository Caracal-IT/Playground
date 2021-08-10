using System.Collections.Generic;
using System.Collections.Immutable;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Helpers;
using PaymentEngine.Model;
using PaymentEngine.Stores;
using PaymentEngine.UseCases.Payments.Callback;
using PaymentEngine.UseCases.Payments.ExportData;
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
        public async Task<XmlDocument> ProcessXmlCallback([FromServices] CallbackUseCase useCase, [FromRoute] string reference, CancellationToken token) {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var body = await reader.ReadToEndAsync();
            var request = new CallbackRequest {
                Data = body,
                Reference = reference
            };
            var resp = await useCase.ExecuteAsync(request, token);
            
            var result = new XmlDocument();
            result.LoadXml(resp);
            
            return result;
        }
        
        [HttpPost("process/callback")]
        public async Task<Callback> ProcessCallback(CancellationToken token) {
            using var reader = new StreamReader(Request.Body, Encoding.UTF8);
            var textFromBody = await reader.ReadToEndAsync();

            return new Callback { Reference = "123456"};
        }
        
        [HttpPost("export-data")]
        public async Task<ExportDataResponse> ExportDataAsync([FromServices] ExportDataUseCase useCase, ExportDataRequest request, CancellationToken token) => 
            await useCase.ExecuteAsync(request, token);
        
    }

    [XmlRoot("callback")]
    public class Callback {
        [XmlElement("ref")]
        public string Reference { get; set; }
        [XmlElement("status")]
        public string Status { get; set; }
    }
}