using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.Core;
using Playground.PaymentEngine.Store.Terminals;
using Playground.PaymentEngine.Store.Terminals.Model;

namespace Playground.PaymentEngine.Store.File.Terminals {
    public class FileTerminalStore: FileStore, TerminalStore {
        private readonly TerminalData _data;
        private readonly ICacheService _cacheService;
        
        public FileTerminalStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _data = GetRepository<TerminalData>();
        }

        public Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken) {
            var result = _cacheService.GetValue($"{nameof(GetActiveAccountTypeTerminalsAsync)}_{accountTypeId}", () =>
                _data.TerminalMaps
                    .Join(_data.Terminals,
                        tm => tm.TerminalId,
                        t => t.Id,
                        (tm, t) => new { Map = tm, Terminal = t }
                    )
                    .Where(t => t.Map.Enabled && t.Map.AccountTypeId == accountTypeId)
                    .OrderBy(t => t.Map.Order)
                    .Select(t => t.Terminal)
            );

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Terminal>> GetTerminalsAsync(CancellationToken cancellationToken) {
            var result = _cacheService.GetValue(nameof(GetTerminalsAsync), () => _data.Terminals.AsEnumerable());
            return Task.FromResult(result);
        }

        public Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken) {
            _data.TerminalResults.AddRange(results);
            return Task.CompletedTask;
        }
    }
}