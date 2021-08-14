using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router {
    public interface Engine {
        Task<List<string>> ProcessAsync<T>(Request<T> request, CancellationToken token) where T : class;
    }
}