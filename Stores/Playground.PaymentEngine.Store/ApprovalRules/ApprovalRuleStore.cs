namespace Playground.PaymentEngine.Store.ApprovalRules;

using Playground.PaymentEngine.Store.ApprovalRules.Model;

public interface ApprovalRuleStore {
    Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<ApprovalRuleHistory>> GetLastRunApprovalRulesAsync(CancellationToken cancellationToken);
    Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken);
}