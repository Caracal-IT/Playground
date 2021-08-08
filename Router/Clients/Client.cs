using System.Threading.Tasks;

namespace Router.Clients {
    public interface Client {
        public Task<string> SendAsync(string message);
    }
}