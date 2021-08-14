using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router {
    public interface TerminalStore {
        Task<Terminal> GetTerminalAsync(string name, CancellationToken token);
    }
}