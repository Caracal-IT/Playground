using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.PaymentEngine.Stores.Terminals.Model;

namespace Playground.PaymentEngine.Stores.Terminals {
    public interface TerminalStore {
        Task<IEnumerable<Terminal>> GetActiveAccountTypeTerminalsAsync(long accountTypeId, CancellationToken cancellationToken);
        Task<IEnumerable<Terminal>> GetTerminalsAsync(CancellationToken cancellationToken);
        Task LogTerminalResultsAsync(IEnumerable<TerminalResult> results, CancellationToken cancellationToken);
    }
}