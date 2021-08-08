using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PaymentEngine.Helpers;
using PaymentEngine.Model;
using PaymentEngine.Stores;
using Router;
using Terminal = PaymentEngine.Model.Terminal;

namespace PaymentEngine.UseCases.Payments.Process {
    public class ProcessUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly RouterEngine _engine;
        private readonly Store _store;
        
        public ProcessUseCase(PaymentStore paymentStore, RouterEngine engine) {
            _paymentStore = paymentStore;
            _engine = engine;
            _store = _paymentStore.GetStore();
        }

        public async Task<ProcessResponse> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken) {
            var allocations = request.Allocations
                                     .Select(GetAllocationData)
                                     .Where(a => a.AccountId > 0)
                                     .ToList();

            var items = (request.Consolidate ? GetConsolidated() : GetExportData()).ToList();

            await Task.WhenAll(items.Select(Export));
            UpdateStatuses();
            
            return new ProcessResponse(items.SelectMany(i => i.Response));
            
            ExportAllocation GetAllocationData(long allocationId) {
                var allocation = _store.Allocations.AllocationList.FirstOrDefault(a => a.Id == allocationId)??new Allocation();
                var account = _store.Accounts.AccountList.FirstOrDefault(a => a.Id == allocation.AccountId)??new Account();
             
                return new ExportAllocation {
                    AllocationId = allocation.Id,
                    Amount = allocation.Amount + allocation.Charge,
                    AccountId = account.Id,
                    AccountTypeId = account.AccountTypeId,
                    CustomerId = account.CustomerId
                };
            }
            
            async Task Export(ExportData data) {
                var req = new Request {
                    Data = Serializer.Serialize(data),
                    Terminals = GetTerminals().ToDictionary(i => i.Name, i => i.RetryCount)
                };

                var response = await _engine.ProcessAsync(req, cancellationToken);
                var result = response.Select(Serializer.DeSerialize<ExportResponse>).ToList();
                data.Response.AddRange(result);
                
                IEnumerable<Terminal> GetTerminals() =>
                    _store.TerminalMaps
                        .TerminalMapList
                        .Join(_store.Terminals.TerminalList,
                            tm => tm.TerminalId,
                            t => t.Id,
                            (tm, t) => new { Map = tm, Terminal = t }
                        )
                        .Where(t => t.Map.Enabled && t.Map.AccountTypeId == data.AccountTypeId)
                        .OrderBy(t => t.Map.Order)
                        .Select(t => t.Terminal);
            }
            
            void UpdateStatuses() {
                items.ForEach(SetStatus);
                
                void SetStatus(ExportData item) {
                    item.Allocations.ForEach(SetAllocationStatus);
                    
                    void SetAllocationStatus(ExportAllocation exportItem) {
                        var allocation = _store.Allocations
                                               .AllocationList
                                               .FirstOrDefault(a => a.Id == exportItem.AllocationId);
                        
                        if (allocation != null) allocation.AllocationStatusId = item.Response.Any(i => i.Code == "00") ? 4 : 5;
                    }
                }
            }

            IEnumerable<ExportData> GetConsolidated() =>
                allocations.GroupBy(a => new { customerId = a.CustomerId, accountId = a.AccountId })
                    .Select(a => new ExportData {
                        Allocations = a.Select(i => i).ToList(),
                        Amount = a.Sum(i => i.Amount),
                        AccountTypeId = a.First().AccountTypeId,
                        Reference = $"CREF_{a.Key.accountId}_{a.Key.customerId}"
                    });

            IEnumerable<ExportData> GetExportData() =>
                allocations.Select(a => new ExportData {
                    Allocations = new List<ExportAllocation> { a },
                    Amount = a.Amount,
                    AccountTypeId = a.AccountTypeId,
                    Reference = $"REF_{a.AllocationId}_{a.AccountId}_{a.CustomerId}"
                });
        }
    }
}