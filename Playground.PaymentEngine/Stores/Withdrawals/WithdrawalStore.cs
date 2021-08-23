using System.Collections.Generic;
using Playground.PaymentEngine.Stores.Withdrawals.Model;

namespace Playground.PaymentEngine.Stores.Withdrawals {
    public interface WithdrawalStore {
        IEnumerable<Withdrawal> GetWithdrawals(IEnumerable<long> withdrawalIds);
        IEnumerable<Withdrawal> GetWithdrawalGroupWithdrawals(long id);
        IEnumerable<WithdrawalGroup> GetWithdrawalGroups(IEnumerable<long> withdrawalGroupIds);
        WithdrawalGroup GetWithdrawalGroup(long id);
    }
}