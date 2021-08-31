using Playground.Core;
using Playground.PaymentEngine.Store.Deposits;

namespace Playground.PaymentEngine.Store.File.Deposits {
    public class FileDepositStore: FileStore, DepositStore {
        private readonly DepositData _data;
        private readonly ICacheService _cacheService;
        
        public FileDepositStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _data = GetRepository<DepositData>();
        }
    }
}