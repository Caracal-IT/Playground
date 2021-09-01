using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.Core;
using Playground.PaymentEngine.Store.Deposits;
using Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Store.File.Deposits {
    public class FileDepositStore: FileStore, DepositStore {
        private readonly DepositData _data;
        private readonly ICacheService _cacheService;
        
        public FileDepositStore(ICacheService cacheService) {
            _cacheService = cacheService;
            _data = GetRepository<DepositData>();
        }

        public Task<IEnumerable<Deposit>> GetWithdrawalsAsync(CancellationToken cancellationToken) =>
            Task.FromResult(_data.Deposits.AsEnumerable());
    }
}