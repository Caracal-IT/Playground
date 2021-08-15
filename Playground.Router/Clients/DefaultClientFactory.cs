namespace Playground.Router.Clients {
    public class DefaultClientFactory: ClientFactory {
        private readonly HttpTerminalClient _httpClient;
        private readonly StreamTerminalClient _streamClient;

        public DefaultClientFactory(HttpTerminalClient httpClient, StreamTerminalClient streamClient) {
            _httpClient = httpClient;
            _streamClient = streamClient;
        }

        public Client Create(Terminal terminal) => terminal.Type.ToLower() switch {
            "http" => _httpClient,
            "stream" => _streamClient,
            _ => _httpClient
        };
    }
}