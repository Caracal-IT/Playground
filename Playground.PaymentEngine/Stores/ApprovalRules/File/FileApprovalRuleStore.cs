using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules.File {
    public class FileApprovalRuleStore: FileStore, ApprovalRuleStore {
        private readonly ApprovalRuleRepository _repository;

        public FileApprovalRuleStore() =>
            _repository = GetRepository<ApprovalRuleRepository>();
        
        public Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken) {
            var result = _repository.ApprovalRuleRuleHistories
                                    .Where(h => withdrawalGroupIds.Contains(h.WithdrawalGroupId));

            return Task.FromResult(result);
        }

        public Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
            _repository.ApprovalRuleRuleHistories.AddRange(histories);

            return Task.CompletedTask;
        }
    }
}