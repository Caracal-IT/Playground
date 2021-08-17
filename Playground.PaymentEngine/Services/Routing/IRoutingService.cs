using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router;

namespace Playground.PaymentEngine.Services.Routing {
    public interface IRoutingService {
        Task<IEnumerable<Response>> Send(RoutingRequest request, CancellationToken cancellationToken);
    }
}