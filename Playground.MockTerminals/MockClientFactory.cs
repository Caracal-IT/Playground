using Playground.Router.Clients;

namespace Playground.MockTerminals {
    public class MockClientFactory: ClientFactory {
        public Client Create(string name) => name switch {
            "Terminal1" => new HttpTerminalClient(),
            "Terminal2" => new Terminal2Client(),
            "Terminal3" => new Terminal3Client(),
            _ => new Terminal3Client()
        };
    }
}