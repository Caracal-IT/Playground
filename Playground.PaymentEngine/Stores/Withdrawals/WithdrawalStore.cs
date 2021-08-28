using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals {
    public interface WithdrawalStore {
        Task<Withdrawal> CreateWithdrawalAsync(Withdrawal withdrawal, CancellationToken cancellationToken);
        
        Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
        Task DeleteWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
        Task UpdateWithdrawalStatusAsync(IEnumerable<long> withdrawalIds, long statusId, CancellationToken cancellationToken);
        
        
        
        Task<IEnumerable<WithdrawalGroup>> GroupWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
        
        Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken);
        Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken);
    }
}