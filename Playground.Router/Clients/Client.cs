using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router.Clients {
    public interface Client {
        public Task<string> SendAsync(Guid transactionId, Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken);
    }
}