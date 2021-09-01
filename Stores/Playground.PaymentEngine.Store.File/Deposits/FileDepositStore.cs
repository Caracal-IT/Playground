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

        public Task<IEnumerable<Deposit>> GetDepositsAsync(CancellationToken cancellationToken) =>
            Task.FromResult(_data.Deposits.AsEnumerable());

        public async Task<IEnumerable<Deposit>> GetDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken) {
            var deposits = await GetDepositsAsync(cancellationToken);
            return deposits.Where(i => depositIds.Contains(i.Id));
        }

        private static readonly object CreateLock = new();
        public Task<Deposit> CreateDepositAsync(Deposit deposit, CancellationToken cancellationToken) {
            long id;
            
            lock (CreateLock) {
                id = _data.Deposits.Max(w => w.Id) + 1;
            }

            deposit.Id = id;
            _data.Deposits.Add(deposit);
            
            return Task.FromResult(deposit);
        }
    }
}