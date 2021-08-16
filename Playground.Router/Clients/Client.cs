using System;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router.Old;

namespace Playground.Router.Clients {
    public interface Client {
        public Task<string> SendAsync(Configuration configuration, string message, CancellationToken cancellationToken);
    }
}