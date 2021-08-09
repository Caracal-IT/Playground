using Router.Clients;

namespace PaymentEngine.Terminals.Clients {
    public class MockClientFactory: ClientFactory {
        public Client Create(string name) => name switch {
            "Terminal1" => new Terminal1Client(),
            "Terminal2" => new Terminal2Client(),
            "Terminal3" => new Terminal3Client(),
            _ => new Terminal3Client()
        };
    }
}