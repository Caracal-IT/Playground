using System.Threading;
using System.Threading.Tasks;

namespace Playground.Router.Clients {
    public interface Client {
        public Task<string> SendAsync(Configuration configuration, string message, CancellationToken cancellationToken);
    }
}