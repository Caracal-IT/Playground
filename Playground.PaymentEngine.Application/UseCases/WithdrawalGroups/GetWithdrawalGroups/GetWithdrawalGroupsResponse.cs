using System;

namespace Playground.PaymentEngine.Application.UseCases.WithdrawalGroups.GetWithdrawalGroups {
    public class GetWithdrawalGroupsResponse {
        public IEnumerable<WithdrawalGroup> WithdrawalGroups { get; set; } = Array.Empty<WithdrawalGroup>();
    }
}