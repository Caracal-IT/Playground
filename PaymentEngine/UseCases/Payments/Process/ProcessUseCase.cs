using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PaymentEngine.Helpers;
using PaymentEngine.Stores;
using Router;
using Terminal = PaymentEngine.Model.Terminal;

namespace PaymentEngine.UseCases.Payments.Process {
    public class ProcessUseCase {
        private readonly PaymentStore _paymentStore;
        private readonly RouterEngine _engine;
        
        public ProcessUseCase(PaymentStore paymentStore, RouterEngine engine) {
            _paymentStore = paymentStore;
            _engine = engine;
        }

        public async Task<ProcessResponse> ExecuteAsync(ProcessRequest request, CancellationToken cancellationToken) {
            var responses = await Task.WhenAll(request.Allocations.Select(Export));
            return new ProcessResponse { Message = string.Join("\n", responses) };

            async Task<string> Export(long allocationId) {
                var store = _paymentStore.GetStore();
                var allocation = store.Allocations.AllocationList.FirstOrDefault(a => a.Id == allocationId);
                
                if (allocation == null)
                    return string.Empty;
                
                var data = GetData();
                
                var req = new Request {
                    Data = Serializer.Serialize(data),
                    Terminals = GetTerminals().ToDictionary(i => i.Name, i => i.RetryCount)
                };

                return await _engine.ProcessAsync(req, cancellationToken);

                ExportData GetData() {
                    var account = store.Accounts.AccountList.First(a => a.Id == allocation.AccountId);
                    var accountType = store.AccountTypes.AccountTypeList.First(a => a.Id == account.AccountTypeId);

                    return new ExportData {
                        Method = accountType.Name,
                        AccountTypeId = accountType.Id,
                        Amount = allocation.Amount + allocation.Charge,
                        MetaData = account.MetaData,
                        Reference = $"REF_{allocationId}_{account.Id}_{account.CustomerId}"
                    };
                }

                IEnumerable<Terminal> GetTerminals() =>
                     store.TerminalMaps
                          .TerminalMapList
                          .Join(store.Terminals.TerminalList,
                              tm => tm.TerminalId,
                              t => t.Id,
                              (tm, t) => new { Map = tm, Terminal = t }
                          )
                          .Where(t => t.Map.Enabled && t.Map.AccountTypeId == data.AccountTypeId)
                          .OrderBy(t => t.Map.Order)
                          .Select(t => t.Terminal);
            }
        }
    }
}