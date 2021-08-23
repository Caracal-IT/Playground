using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Playground.PaymentEngine.Services.CacheService;
using Playground.PaymentEngine.Stores.Terminals.Model;

namespace Playground.PaymentEngine.Stores.Terminals.File {
    public class FileTerminalStore: TerminalStore {
        private TerminalRepository _repository;
        private readonly ICacheService _cacheService;
        
        public FileTerminalStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _repository = GetRepository();
        }

        public IEnumerable<Terminal> GetActiveAccountTypeTerminals(long accountTypeId) =>
            _cacheService.GetValue($"{nameof(GetTerminals)}_{accountTypeId}", () => 
                _repository.TerminalMaps
                    .Join(_repository.Terminals,
                        tm => tm.TerminalId,
                        t => t.Id,
                        (tm, t) => new { Map = tm, Terminal = t }
                    )
                    .Where(t => t.Map.Enabled && t.Map.AccountTypeId == accountTypeId)
                    .OrderBy(t => t.Map.Order)
                    .Select(t => t.Terminal)
                    .ToList()
            );

        public IEnumerable<Terminal> GetTerminals() =>
            _cacheService.GetValue(nameof(GetTerminals), () => _repository.Terminals);
        
        public void LogTerminalResults(IEnumerable<TerminalResult> results) =>
            _repository.TerminalResults.AddRange(results);
        
        private static TerminalRepository GetRepository() {
            var path = Path.Join("Stores", "Terminals", "File", "repository.xml");
            using var fileStream = new FileStream(path, FileMode.Open);
            return (TerminalRepository) new XmlSerializer(typeof(TerminalRepository)).Deserialize(fileStream);
        }
    }
}