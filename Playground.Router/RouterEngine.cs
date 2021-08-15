using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router.Clients;

namespace Playground.Router {
    public class RouterEngine: Engine {
        private readonly TerminalStore _store;
        private readonly ClientFactory _factory;
        private readonly TerminalExtensions _extensions;
        
        public RouterEngine(TerminalStore store, TerminalExtensions extensions, ClientFactory factory) {
            _store = store;
            _extensions = extensions;
            _factory = factory;
        }

        public async Task<List<string>> ProcessAsync<T>(Guid transactionId, Request<T> request, CancellationToken cancellationToken) where T : class =>
            await TerminalProcessor<T>.ProcessAsync(transactionId, request, _store, _extensions, _factory, cancellationToken);
    }
}