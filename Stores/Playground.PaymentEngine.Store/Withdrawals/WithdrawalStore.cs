namespace Playground.PaymentEngine.Store.Withdrawals;

using Model;

public interface WithdrawalStore {
    Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken);
    Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(CancellationToken cancellationToken);
    Task<IQueryable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
    Task DeleteWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
    Task UpdateWithdrawalStatusAsync(IEnumerable<long> withdrawalIds, long statusId, CancellationToken cancellationToken);
    Task<IEnumerable<WithdrawalGroup>> GroupWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
    Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken);
    Task UnGroupWithdrawalsAsync(long withdrawalGroupId, CancellationToken cancellationToken);
    Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(CancellationToken cancellationToken);
    Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken);
    Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken);
    Task<WithdrawalGroup?> AppendWithdrawalGroupsAsync(long Id, IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
    WithdrawalStore Clone();
}