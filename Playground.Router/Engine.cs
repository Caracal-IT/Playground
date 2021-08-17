using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router {
    public interface Engine {
        Task<IEnumerable<Response>> ProcessAsync(Request request, CancellationToken cancellationToken);

    }
}