namespace Playground.Router;

public interface Engine {
    Task<IEnumerable<Response>> ProcessAsync(Request request, CancellationToken cancellationToken);
}