using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Router {
    public interface RouterEngine {
        Task<List<string>> ProcessAsync<T>(Request<T> request, CancellationToken token) where T : class;
    }
}