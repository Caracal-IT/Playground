using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals.File {
    public class FileWithdrawalStore: FileStore, WithdrawalStore {
        private readonly WithdrawalRepository _repository;

        public FileWithdrawalStore() => 
            _repository = GetRepository<WithdrawalRepository>();

        public Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken) {
            var result =  _repository.Withdrawals
                                     .Where(w => withdrawalIds.Contains(w.Id));

            return Task.FromResult(result);
        }

        public async Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken) {
            var group = _repository.WithdrawalGroups.FirstOrDefault(g => g.Id == id)??new WithdrawalGroup();
            return await GetWithdrawalsAsync(group.WithdrawalIds, cancellationToken);
        }
        
        public Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) {
            var result = _repository.WithdrawalGroups
                                    .Where(g => withdrawalGroupIds.Contains(g.Id));

            return Task.FromResult(result);
        }

        public Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken) {
            var result =  _repository.WithdrawalGroups
                                     .FirstOrDefault(g => g.Id == id);

            return Task.FromResult(result);
        }
    }
}