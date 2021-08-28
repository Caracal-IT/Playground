using System.Collections.Generic;

namespace Playground.PaymentEngine.Models.Withdrawals {
    public class GroupWithdrawalRequest {
        public List<long> Withdrawals { get; set; } = new();
    }
}