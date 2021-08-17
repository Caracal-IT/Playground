using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router.Clients;

namespace Playground.Router {
    public class RouterEngine : Engine {
        private readonly ClientFactory _factory;

        public RouterEngine(ClientFactory factory) => _factory = factory;

        public async Task<IEnumerable<Response>> ProcessAsync(Request request, CancellationToken cancellationToken) =>
            await Transaction.ProcessAsync(request,  _factory, cancellationToken);
    }
}