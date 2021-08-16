using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router.Old {
    public interface TerminalStore {
        Task<IEnumerable<OldTerminal>> GetTerminalsAsync(IEnumerable<string> terminals, CancellationToken cancellationToken);
    }
}