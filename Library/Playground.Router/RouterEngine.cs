namespace Playground.Router;

using Clients;

public class RouterEngine : Engine {
    private readonly ClientFactory _factory;

    public RouterEngine(ClientFactory factory) => _factory = factory;

    public async Task<IEnumerable<Response>> ProcessAsync(Request request, CancellationToken cancellationToken) =>
        await Transaction.ProcessAsync(request,  _factory, cancellationToken);
}