using System.Threading;
using System.Threading.Tasks;

namespace Router {
    public interface TerminalStore {
        Task<Terminal> GetTerminalAsync(string name, CancellationToken token);
    }
}