using System.Linq;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules.File {
    public class FileApprovalRuleStore: FileStore, ApprovalRuleStore {
        private readonly ApprovalRuleData _data;

        public FileApprovalRuleStore() =>
            _data = GetRepository<ApprovalRuleData>();
        
        public Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken) => 
            Task.FromResult(_data.ApprovalRuleRuleHistories.AsEnumerable());

        public Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
            _data.ApprovalRuleRuleHistories.AddRange(histories);

            return Task.CompletedTask;
        }
    }
}