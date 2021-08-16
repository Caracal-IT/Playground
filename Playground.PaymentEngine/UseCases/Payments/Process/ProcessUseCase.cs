using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Extensions;
using Playground.PaymentEngine.Helpers;
using Playground.PaymentEngine.Model;
using Playground.PaymentEngine.Stores;
using Playground.Router;
using Playground.Router.Old;
using static Playground.PaymentEngine.Helpers.Hashing;
using static Playground.Xml.Serialization.Serializer;
using Setting = Playground.Router.Setting;
using Terminal = Playground.Router.Terminal;

namespace Playground.PaymentEngine.UseCases.Payments.Process {
    public class ProcessUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly TerminalExtensions _extensions;
        private readonly Engine _engine;
        
        public ProcessUseCase(PaymentStore paymentStore, TerminalExtensions extensions, Engine engine) {
            _paymentStore = paymentStore;
            _extensions = extensions;
            _engine = engine;
        }

        public async Task<ProcessResponse> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken) {
            var transactionId = Guid.NewGuid();
            var allocations = _paymentStore.GetExportAllocations(request.Allocations).ToList();
            var items = (request.Consolidate ? GetConsolidated() : GetExportData()).ToList();
            
            await items.Select(Export).WhenAll(maxConcurrentRequests: 50);
            
            UpdateStatuses();
            SaveResults();
                
            return new ProcessResponse(items.Select(i => i.Response.Last()));

            async Task Export(ExportData data) {
                /*
                var req2 = new Request<ExportData> {
                    Name = nameof(ProcessUseCase),
                    Payload = data,
                    Terminals = _paymentStore.GetActiveAccountTypeTerminals(data.AccountTypeId).Select(i => i.Name)
                };

               var response =  await _engine.ProcessAsync(transactionId, req2, cancellationToken);
               var result = response.Select(DeSerialize<ExportResponse>);
               data.Response.AddRange(result);
*/
               var terminals = _paymentStore.GetActiveAccountTypeTerminals(data.AccountTypeId)
                   .Select(async t => new Terminal {
                       Name = t.Name,
                       Settings = t.Settings.Select(s => new Setting(s.Name, s.Value)),
                       Type = t.Type,
                       RetryCount = t.RetryCount,
                       Xslt = await GetXslt(t.Name)
                   });
               
                var req = new Request { 
                    TransactionId = transactionId,
                    Name = nameof(ProcessUseCase),
                    Payload = data.Serialize(),
                    Extensions = _extensions,
                    Terminals = await Task.WhenAll(terminals)
               };

               var response = await _engine.ProcessAsync2(req, cancellationToken);


               async Task<string> GetXslt(string name) {
                   var path = Path.Join("Terminals", "Templates", $"{name}.xslt");

                   if (!File.Exists(path)) return string.Empty;

                   return await path.ReadFromFileAsync(cancellationToken);
               }
            }

            void SaveResults() {
                var results = items.SelectMany(i => i.Response).Select(MapResults);
                _paymentStore.LogTerminalResults(results);
            }

            TerminalResult MapResults(ExportResponse response) {
                return new TerminalResult {
                    Code = response.Code,
                    Date = DateTime.Now,
                    Message = response.Message,
                    Reference = response.Reference,
                    Success = response.Code == "00",
                    Terminal = response.Terminal,
                    MetaData = response.MetaData
                };
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