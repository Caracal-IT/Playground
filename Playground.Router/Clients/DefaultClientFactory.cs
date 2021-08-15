using System.Net.Http;

namespace Playground.Router.Clients {
    public class DefaultClientFactory: ClientFactory {
        private readonly HttpTerminalClient _httpClient;

        public DefaultClientFactory(HttpTerminalClient httpClient) =>
            _httpClient = httpClient;
        
        public Client Create(Terminal terminal) => terminal.Type.ToLower() switch {
            "http" => _httpClient,
            _ => _httpClient
        };
    }
}