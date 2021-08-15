using System.Net.Http;

namespace Playground.Router.Clients {
    public class DefaultClientFactory: ClientFactory {
        private readonly HttpClient _httpClient;

        public DefaultClientFactory(HttpClient httpClient) =>
            _httpClient = httpClient;
        
        public Client Create(Terminal terminal) => terminal.Type.ToLower() switch {
            "http" => new HttpTerminalClient(_httpClient),
            _ => new HttpTerminalClient(_httpClient)
        };
    }
}