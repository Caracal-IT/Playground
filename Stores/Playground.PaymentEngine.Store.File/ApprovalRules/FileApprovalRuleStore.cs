using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.ApprovalRules;
using Playground.PaymentEngine.Store.ApprovalRules.Model;

namespace Playground.PaymentEngine.Store.File.ApprovalRules {
    public class FileApprovalRuleStore: FileStore, ApprovalRuleStore {
        private readonly ApprovalRuleData _data;

        public FileApprovalRuleStore() =>
            _data = GetRepository<ApprovalRuleData>();
        
        public Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken) => 
            Task.FromResult(_data.ApprovalRuleRuleHistories.AsEnumerable());

        public Task<IEnumerable<ApprovalRuleHistory>> GetLastRunApprovalRulesAsync(CancellationToken cancellationToken) => 
            Task.FromResult(_data.ApprovalRuleRuleHistories.GroupBy(r => r.WithdrawalGroupId, (_, h) => h.Last()));
        
        public Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
            _data.ApprovalRuleRuleHistories.AddRange(histories);

            return Task.CompletedTask;
        }
    }
}