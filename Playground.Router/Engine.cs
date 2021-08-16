using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Playground.Router.Old;

namespace Playground.Router {
    public interface Engine {
        Task<List<string>> ProcessAsync<T>(Guid transactionId, Old.Request<T> request, CancellationToken token) where T : class;




        Task<Response> ProcessAsync2(Request request, CancellationToken cancellationToken);

    }
}