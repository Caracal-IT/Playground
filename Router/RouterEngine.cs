using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Router {
    public interface RouterEngine {
        Task<List<string>> ProcessAsync(Request request, CancellationToken token);
    }
}