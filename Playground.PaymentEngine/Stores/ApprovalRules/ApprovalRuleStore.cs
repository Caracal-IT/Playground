using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.ApprovalRules.Model;

namespace Playground.PaymentEngine.Stores.ApprovalRules {
    public interface ApprovalRuleStore {
        Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);

        Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken);
    }
}