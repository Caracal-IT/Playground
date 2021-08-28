using System.Collections.Generic;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GroupWithdrawals {
    public class GroupWithdrawalsResponse {
        public IEnumerable<WithdrawalGroup> WithdrawalGroups { get; set; } 
    }
}