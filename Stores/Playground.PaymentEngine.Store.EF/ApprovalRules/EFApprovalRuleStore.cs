// ReSharper disable InconsistentNaming
namespace Playground.PaymentEngine.Store.EF.ApprovalRules;

public partial class EFApprovalRuleStore: DbContext, ApprovalRuleStore {
    public Task<IEnumerable<ApprovalRuleHistory>> GetRuleHistoriesAsync(CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task<IEnumerable<ApprovalRuleHistory>> GetLastRunApprovalRulesAsync(CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }

    public Task AddRuleHistoriesAsync(IEnumerable<ApprovalRuleHistory> histories, CancellationToken cancellationToken) {
        throw new System.NotImplementedException();
    }
}