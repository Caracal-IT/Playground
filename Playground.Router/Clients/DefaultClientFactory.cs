using Playground.Router.Clients.File;
using Playground.Router.Old;

namespace Playground.Router.Clients {
    public class DefaultClientFactory: ClientFactory {
        private readonly HttpTerminalClient _httpClient;
        private readonly FileTerminalClient _fileClient;

        public DefaultClientFactory(HttpTerminalClient httpClient, FileTerminalClient fileClient) {
            _httpClient = httpClient;
            _fileClient = fileClient;
        }

        public Client Create(Terminal terminal) => terminal.Type.ToLower() switch {
            "http" => _httpClient,
            "stream" => _fileClient,
            _ => _httpClient
        };
    }
}