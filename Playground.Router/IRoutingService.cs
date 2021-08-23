using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router {
    public record RoutingRequest(Guid TransactionId, string Name, string Payload, IEnumerable<string> Terminals);
    public interface IRoutingService {
        Task<IEnumerable<Response>> SendAsync(RoutingRequest request, CancellationToken cancellationToken);
    }
}