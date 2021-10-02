namespace Playground.Router.Services;

public record RoutingRequest(Guid TransactionId, string Name, string Payload, IEnumerable<string> Terminals);

public interface IRoutingService {
    Task<IEnumerable<Response>> SendAsync(RoutingRequest request, CancellationToken cancellationToken);
}