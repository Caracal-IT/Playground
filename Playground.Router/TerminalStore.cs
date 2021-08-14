using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router {
    public interface TerminalStore {
        Task<IEnumerable<Terminal>> GetTerminalsAsync(IEnumerable<string> terminals, CancellationToken cancellationToken);
    }
}