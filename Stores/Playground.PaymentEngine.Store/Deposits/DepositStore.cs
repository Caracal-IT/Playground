using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Store.Deposits.Model;

namespace Playground.PaymentEngine.Store.Deposits {
    public interface DepositStore {
        Task<IEnumerable<Deposit>> GetDepositsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<Deposit>> GetDepositsAsync(IEnumerable<long> depositIds, CancellationToken cancellationToken);
        Task<Deposit> CreateDepositAsync(Deposit deposit, CancellationToken cancellationToken);
    }
}