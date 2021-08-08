using System.Threading;
using System.Threading.Tasks;

namespace Router {
    public interface RouterEngine {
        Task<string> ProcessAsync(Request request, CancellationToken token);
    }
}