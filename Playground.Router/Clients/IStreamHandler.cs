using System;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router.Clients {
    public interface IStreamHandler {
        Task<string> WriteAsync(Guid transactionId, Configuration configuration, string message, Terminal terminal, CancellationToken cancellationToken);
    }
}