using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.Terminals.Model;

namespace Playground.PaymentEngine.Stores.Terminals.File {
    public class FileTerminalStore: FileStore, TerminalStore {
        private readonly TerminalRepository _repository;
        private readonly ICacheService _cacheService;
        
        public FileTerminalStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _repository = GetRepository<TerminalRepository>();
        }

        public Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken) {
            var result = _cacheService.GetValue($"{nameof(GetActiveAccountTypeTerminalsAsync)}_{accountTypeId}", () =>
                _repository.TerminalMaps
                    .Join(_repository.Terminals,
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
            var result = _cacheService.GetValue(nameof(GetTerminalsAsync), () => _repository.Terminals.AsEnumerable());
            return Task.FromResult(result);
        }

        public Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken) {
            _repository.TerminalResults.AddRange(results);
            return Task.CompletedTask;
        }
    }
}