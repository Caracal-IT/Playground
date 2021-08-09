using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentEngine.Model;
using PaymentEngine.Stores;
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
        
        [HttpPost("export-data")]
        public async Task<ExportDataResponse> ExportDataAsync([FromServices] ExportDataUseCase useCase, ExportDataRequest request, CancellationToken token) => 
            await useCase.ExecuteAsync(request, token);
        
    }
}