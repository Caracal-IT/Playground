using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using PaymentEngine.Helpers;
using PaymentEngine.Model;
using PaymentEngine.Stores;
using PaymentEngine.Terminals.Clients;
using Router;
using static PaymentEngine.Helpers.Serializer;

namespace PaymentEngine.UseCases.Payments.ExportData {
    public class ExportDataUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly RouterEngine _engine;
        
        public ExportDataUseCase(PaymentStore paymentStore, RouterEngine engine) {
            _paymentStore = paymentStore;
            _engine = engine;
        }

        public async Task<ExportDataResponse> ExecuteAsync(ExportDataRequest request, CancellationToken cancellationToken) {
            var allocations = _paymentStore.GetExportAllocations(request.Allocations).ToList();
            var items = (request.Consolidate ? GetConsolidated() : GetExportData()).ToList();
            
            await items.Select(Export).WhenAll(maxConcurrentRequests: 50);
            
            return new ExportDataResponse(items.SelectMany(i => i.Response));

            async Task Export(ExportData data) {
                var req = new Request {
                    RequestType = (int) RequestType.Export,
                    Data = Serialize(data), 
                    Terminals = _paymentStore.GetActiveAccountTypeTerminals(data.AccountTypeId)
                                             .ToDictionary(i => i.Name, i => i.RetryCount)
                };
                
                var response = await _engine.ExportDataAsync(req, request.PreferredTerminals, cancellationToken);
                data.Response.Add(new ExportResponseData{Allocations = data.Allocations, Data = response});
            }

            IEnumerable<ExportData> GetConsolidated() =>
                allocations
                    .GroupBy(a => new { customerId = a.CustomerId, accountId = a.AccountId })
                    .Select(a => new ExportData {
                        Allocations = a.Select(i => i).ToList(),
                        Amount = a.Sum(i => i.Amount),
                        AccountTypeId = a.First().AccountTypeId,
                        Reference = $"CREF_{a.Key.accountId}_{a.Key.customerId}",
                        MetaData = a.First().MetaData
                    });

            IEnumerable<ExportData> GetExportData() =>
                allocations.Select(a => new ExportData {
                    Allocations = new List<ExportAllocation> { a },
                    Amount = a.Amount,
                    AccountTypeId = a.AccountTypeId,
                    Reference = $"REF_{a.AllocationId}_{a.AccountId}_{a.CustomerId}",
                    MetaData = a.MetaData
                });
        }
    }
}