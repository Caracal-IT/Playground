using System;

namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GroupWithdrawals {
    public class GroupWithdrawalsResponse {
        public IEnumerable<WithdrawalGroup> WithdrawalGroups { get; set; } = Array.Empty<WithdrawalGroup>();
    }
}