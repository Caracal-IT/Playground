using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.Router;

using static Playground.PaymentEngine.Helpers.Hashing;
using static Playground.Xml.Serialization.Serializer;

namespace Playground.PaymentEngine.UseCases.Payments.Process {
    public class ProcessUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly Engine _engine;
        
        public ProcessUseCase(PaymentStore paymentStore, Engine engine) {
            _paymentStore = paymentStore;
            _engine = engine;
        }

        public async Task<ProcessResponse> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken) {
            var transactionId = Guid.NewGuid();
            var allocations = _paymentStore.GetExportAllocations(request.Allocations).ToList();
            var items = (request.Consolidate ? GetConsolidated() : GetExportData()).ToList();
            
            await items.Select(Export).WhenAll(maxConcurrentRequests: 50);
            
            UpdateStatuses();
            
            return new ProcessResponse(items.SelectMany(i => i.Response));

            async Task Export(ExportData data) {
                var req2 = new Request<ExportData> {
                    Name = nameof(ProcessUseCase),
                    Payload = data,
                    Terminals = _paymentStore.GetActiveAccountTypeTerminals(data.AccountTypeId).Select(i => i.Name)
                };

               var response =  await _engine.ProcessAsync(transactionId, req2, cancellationToken);
               var result = response.Select(DeSerialize<ExportResponse>);
               data.Response.AddRange(result);
            }
            
            void UpdateStatuses() {
                items.ForEach(SetStatus);
                
                void SetStatus(ExportData data) {
                    var response = data.Response.FirstOrDefault(i => i.Code == "00") ?? new ExportResponse();
                    var statusId = response.Code == "00" ? 4 : 5;
                    var terminal = response.Terminal;
                    
                    data.Allocations.ForEach(SetAllocationStatus);

                    void SetAllocationStatus(ExportAllocation ea) => 
                        _paymentStore.SetAllocationStatus(ea.AllocationId, statusId, terminal, data.Reference);
                }
            }

            IEnumerable<ExportData> GetConsolidated() =>
                allocations
                    .GroupBy(a => new { customerId = a.CustomerId, accountId = a.AccountId })
                    .Select(a => new ExportData {
                        Allocations = a.Select(i => i).ToList(),
                        Amount = a.Sum(i => i.Amount),
                        AccountTypeId = a.First().AccountTypeId,
                        Reference = hash5(string.Join('*', a.Select(i => i.AllocationId))),
                        MetaData = a.First().MetaData
                    });

            IEnumerable<ExportData> GetExportData() =>
                allocations.Select(a => new ExportData {
                    Allocations = new List<ExportAllocation> { a },
                    Amount = a.Amount,
                    AccountTypeId = a.AccountTypeId,
                    Reference = hash5($"{a.AllocationId}"),
                    MetaData = a.MetaData
                });
        }
    }
}