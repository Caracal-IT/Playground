using System.Threading;
using System.Threading.Tasks;

namespace Playground.PaymentEngine.UseCases.Withdrawals.GetWithdrawals {
    public class GetWithdrawalsUseCase {
        public Task<GetWithdrawalsResponse> ExecuteAsync(GetWithdrawalsRequest request, CancellationToken cancellationToken) =>
            Task.FromResult(new GetWithdrawalsResponse{});
    }
}