namespace Playground.Router.Clients;

public interface ClientFactory {
    Client Create(Terminal terminal);
}