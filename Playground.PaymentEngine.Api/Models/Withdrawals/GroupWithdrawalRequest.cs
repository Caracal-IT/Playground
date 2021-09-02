namespace Playground.PaymentEngine.Api.Models.Withdrawals {
    public class GroupWithdrawalRequest {
        public List<long> Withdrawals { get; set; } = new();
    }
}