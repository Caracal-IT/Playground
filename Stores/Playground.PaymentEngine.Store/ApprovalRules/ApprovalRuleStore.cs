using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.ApprovalRules.Model;

namespace Playground.PaymentEngine.Store.ApprovalRules {
    public interface ApprovalRuleStore {
        Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken);
        Task<IEnumerable<ApprovalRuleHistory>> GetLastRunApprovalRulesAsync(CancellationToken cancellationToken);
        Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken);
    }
}