using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals {
    public interface WithdrawalStore {
        Task<IEnumerable<Withdrawal>> GetWithdrawalsAsync(IEnumerable<long> withdrawalIds, CancellationToken cancellationToken);
        Task<IEnumerable<Withdrawal>> GetWithdrawalGroupWithdrawalsAsync(long id, CancellationToken cancellationToken);
        Task<IEnumerable<WithdrawalGroup>> GetWithdrawalGroupsAsync(IEnumerable<long> withdrawalGroupIds, CancellationToken cancellationToken);
        Task<WithdrawalGroup> GetWithdrawalGroupAsync(long id, CancellationToken cancellationToken);
    }
}